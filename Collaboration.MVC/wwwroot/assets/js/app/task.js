$(document).ready(() => {

    var myModal = new bootstrap.Modal(document.getElementById('showModal'), {
        backdrop: 'static',
        keyboard: false
    });

    $(".add-btn").on("click", async (event) => {
        $("#add-btn").text("Create Task").removeClass("btn-warning").addClass("btn-success");
    });

    let editingTaskId = null;

    const boardId = parseInt($("#boardId").val(), 10);

    const loadAssignedUsers = (selectedUserId = null) => {
        $.get("/User/Get", function (users) {
            const select = $("#taskAssignedTo-field").empty().append('<option value="">Assigned To</option>');

            users.forEach(({ id, fullName }) => {
                select.append(`<option value="${id}">${fullName}</option>`);
            });

            if (selectedUserId !== null && !isNaN(selectedUserId)) {
                $("#taskAssignedTo-field").val(selectedUserId).trigger("change");
            }
        }).fail(err => {
            Swal.fire("Error!", "Failed to load users.", "error");
        });
    };

    const table = $("#tasksTable").DataTable({
        ajax: {
            url: `/task/GetAll/?boardId=${boardId}`,
            type: "GET",
            dataSrc: ""
        },
        columns: [
            { data: "id" },
            { data: "title" },
            { data: "assignedTo" },
            { data: "startDate" },
            { data: "endDate" },
            {
                data: "status", render: function (data, type, row) {
                    return getStatusBadge(data);
                }
            },
            {
                data: "priority", render: function (data, type, row) {
                    return getPriorityBadge(data);
                }
            },
            {
                data: null,
                render: ({ id }) => `
                    <button class="btn btn-success btn-sm edit-task" data-id="${id}"><i class="ri-pencil-line"></i></button>
                    <button class="btn btn-danger btn-sm delete-task" data-id="${id}"><i class="ri-delete-bin-line"></i></button>
                `
            }
        ],
        columnDefs: [
            { targets: [7], orderable: false }
        ]
    });

    $("#add-btn").on("click", async (event) => {


        event.preventDefault();

        const task = getTaskFormData();

        if (!isValidTask(task)) {
            Swal.fire("Warning!", "Please fill all fields!", "warning");
            return;
        }

        console.log(task);

        const requestType = editingTaskId ? "PUT" : "POST";

        const url = editingTaskId ? `/task/Update/${editingTaskId}` : "/task/Create";

        try {
            await $.ajax({
                url,
                type: requestType,
                contentType: "application/json",
                data: JSON.stringify(task)
            });

            Swal.fire("Success!", `Task ${editingTaskId ? "updated" : "added"} successfully!`, "success");
            $("#showModal").modal("hide");
            resetForm();
            table.ajax.reload();
        } catch (err) {
            Swal.fire("Error!", `Failed to ${editingTaskId ? "update" : "add"} task.`, "error");
        }
    });

    $(document).on("click", ".edit-task", async function () {
        const taskId = $(this).data("id");
        try {
            const task = await $.get(`/task/Get/${taskId}`);
            populateTaskForm(task);
            editingTaskId = task.id;
            $("#showModal").modal("show");
        } catch (err) {
            Swal.fire("Error!", "Failed to fetch task details.", "error");
        }
    });

    $(document).on("click", ".delete-task", async function () {
        const taskId = $(this).data("id");

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to undo this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "Cancel"
        }).then(async (result) => {
            if (result.isConfirmed) {
                try {
                    await $.ajax({ url: `/task/delete/${taskId}`, type: "DELETE" });
                    Swal.fire("Deleted!", "The task has been deleted.", "success");
                    table.ajax.reload();
                } catch (err) {
                    Swal.fire("Error!", "Failed to delete the task.", "error");
                }
            }
        });
    });

    $("#showModal").on("hidden.bs.modal", resetForm);

    $("#showModal").on("shown.bs.modal", function () {
        const selectedUserId = $("#taskAssignedTo-field").val();
        loadAssignedUsers(selectedUserId);
    });

    function getTaskFormData() {
        return {
            title: $("#taskTilte-field").val(),
            description: $("#tasksDescription-field").val(),
            priority: parseInt($("#taskPriority-field").val(), 10),
            startDate: formatDate($("#taskStartDate-field").val()),
            endDate: formatDate($("#taskEndDate-field").val()),
            assignedTo: parseInt($("#taskAssignedTo-field").val(), 10),
            boardId,
            status: parseInt($("#task-status").val(), 10)
        };
    }

    function isValidTask(task) {
        return Object.values(task).every(value => value);
    }
    
    function formatDate(dateString) {
        return dateString ? new Date(dateString).toISOString() : null;
    }

    function populateTaskForm(task) {
        $("#tasksId").val(task.id);
        $("#taskTilte-field").val(task.title);
        $("#tasksDescription-field").val(task.description);
        $("#taskStartDate-field").val(formatDateForInput(task.startDate));
        $("#taskEndDate-field").val(formatDateForInput(task.endDate));
        $("#task-status").val(task.status);
        $("#taskPriority-field").val(task.priority);

        loadAssignedUsers(task.userId);
        $("#taskAssignedTo-field").val(task.userId);

        $("#add-btn").text("Update Task").removeClass("btn-success").addClass("btn-warning");
    }

    function resetForm() {
        $("#tasksId, #taskTilte-field, #tasksDescription-field, #taskStartDate-field, #taskEndDate-field, #task-status, #taskPriority-field, #taskAssignedTo-field").val("");
        editingTaskId = null;
    }

    function formatDateForInput(dateString) {
        if (!dateString) return "";
        const date = new Date(dateString);
        return date.toISOString().split("T")[0];
    }

    const loadTaskKpis = (boardId) => {
        $.get(`/task/TaskKpis?boardId=${boardId}`, function (data) {
            if (!data) return;

            $("#total-tasks").length && $("#total-tasks").text(data.all);
            $("#pending-tasks").length && $("#pending-tasks").text(data.inProgress);
            $("#completed-tasks").length && $("#completed-tasks").text(data.completed);
            $("#closed-tasks").length && $("#closed-tasks").text(data.closed);
        }).fail(err => {
            Swal.fire("Error!", "Failed to load KPIs.", "error");
        });
    };

    loadTaskKpis(boardId);

    function getStatusBadge(status) {
        let badgeClass;
        let statusText = status.toString().toLowerCase();

        switch (statusText) {
            case "0":
            case "new":
                badgeClass = "bg-primary"; statusText = "New";
                break;
            case "1":
            case "open":
                badgeClass = "bg-secondary"; statusText = "Open";
                break;
            case "2":
            case "in progress":
                badgeClass = "bg-success"; statusText = "In Progress";
                break;
            case "3":
            case "completed":
                badgeClass = "bg-info"; statusText = "Completed";
                break;
            case "4":
            case "closed":
                badgeClass = "bg-warning"; statusText = "Closed";
                break;
            default:
                badgeClass = "bg-dark"; statusText = "Unknown";
        }

        return `<span class="badge ${badgeClass}">${statusText}</span>`;
    }

    function getPriorityBadge(priority) {
        let badgeClass;
        switch (priority.toLowerCase()) {
            case "low": badgeClass = "bg-primary"; break;
            case "medium": badgeClass = "bg-warning"; break;
            case "high": badgeClass = "bg-danger"; break;
            default: badgeClass = "bg-dark";
        }
        return `<span class="badge ${badgeClass}">${priority}</span>`;
    }
});
