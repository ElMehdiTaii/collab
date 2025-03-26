$(document).ready(() => {

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
            console.error("Error loading users:", err);
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
            { data: "userId" },
            { data: "startDate" },
            { data: "endDate" },
            { data: "status" },
            { data: "priority" },
            {
                data: null,
                render: ({ id }) => `
                    <button class="btn btn-warning btn-sm edit-task" data-id="${id}">Edit</button>
                    <button class="btn btn-danger btn-sm delete-task" data-id="${id}">Delete</button>
                `
            }
        ]
    });

    $("#add-btn").on("click", async (event) => {
        event.preventDefault();
        const task = getTaskFormData();

        if (!isValidTask(task)) {
            alert("Please fill all fields!");
            return;
        }

        const requestType = editingTaskId ? "PUT" : "POST";
        const url = editingTaskId ? `${apiBaseUrl}/${editingTaskId}` : "/task/Create";

        try {
            await $.ajax({
                url,
                type: requestType,
                contentType: "application/json",
                data: JSON.stringify(task)
            });
            alert(`Task ${editingTaskId ? "updated" : "added"} successfully!`);
            $("#showModal").modal("hide");
            resetForm();
            table.ajax.reload();
        } catch (err) {
            console.error(`Error ${editingTaskId ? "updating" : "adding"} task:`, err);
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
            console.error("Error fetching task:", err);
        }
    });

    $(document).on("click", ".delete-task", async function () {
        const taskId = $(this).data("id");
        if (confirm("Are you sure you want to delete this task?")) {
            try {
                await $.ajax({ url: `task/delete/${taskId}`, type: "DELETE" });
                alert("Task deleted successfully!");
                table.ajax.reload();
            } catch (err) {
                console.error("Error deleting task:", err);
            }
        }
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
});
