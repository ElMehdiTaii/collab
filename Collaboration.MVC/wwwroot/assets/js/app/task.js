$(document).ready(() => {

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
        console.log(task);
        if (!isValidTask(task)) {
            Swal.fire("Warning!", "Please fill all required fields!", "warning");
            return;
        }

        // Create a FormData object for the form submission
        const formData = new FormData();

        // Add all task fields to the FormData
        Object.keys(task).forEach(key => {
            if (task[key] !== null && task[key] !== undefined) {
                formData.append(key, task[key]);
            }
        });

        // Add files to FormData
        const fileInput = document.getElementById("project-thumbnail-img");
        if (fileInput.files.length > 0) {
            for (let i = 0; i < fileInput.files.length; i++) {
                formData.append("files", fileInput.files[i]);
            }
        }

        const requestType = editingTaskId ? "PUT" : "POST";
        const url = editingTaskId ? `/task/Update/${editingTaskId}` : "/task/Create";

        try {
            await $.ajax({
                url: url,
                type: requestType,
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    Swal.fire("Success!", `Task ${editingTaskId ? "updated" : "created"} successfully!`, "success");
                    $("#showModal").modal("hide");
                    resetForm();
                    table.ajax.reload();
                },
                error: function (error) {
                    console.error("Error:", error);
                    Swal.fire("Error!", `Failed to ${editingTaskId ? "update" : "create"} task.`, "error");
                }
            });
        } catch (err) {
            console.error("Exception:", err);
            Swal.fire("Error!", `An unexpected error occurred.`, "error");
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

        populateAttachments(task.taskAttachements);

        $("#add-btn").text("Update Task").removeClass("btn-success").addClass("btn-warning");
    }
    function resetForm() {
        $("#tasksId").val("");
        $("#taskTilte-field").val("");
        $("#tasksDescription-field").val("");
        $("#taskStartDate-field").val("");
        $("#taskEndDate-field").val("");
        $("#task-status").val("");
        $("#taskPriority-field").val("");
        $("#taskAssignedTo-field").val("");

        // Reset file input
        $("#project-thumbnail-img").val("");

        // Clear attachments display
        $(".pt-3.border-top.border-top-dashed.mt-4 .row.g-3").empty();

        editingTaskId = null;
        $("#add-btn").text("Create Task").removeClass("btn-warning").addClass("btn-success");
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
            case "1":
            case "new":
                badgeClass = "bg-primary"; statusText = "New";
                break;
            case "2":
            case "open":
                badgeClass = "bg-secondary"; statusText = "Open";
                break;
            case "3":
            case "in progress":
                badgeClass = "bg-success"; statusText = "In Progress";
                break;
            case "4":
            case "completed":
                badgeClass = "bg-info"; statusText = "Completed";
                break;
            case "5":
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
    function populateAttachments(attachments) {
        const resourcesRow = $(".pt-3.border-top.border-top-dashed.mt-4 .row.g-3");
        resourcesRow.empty();

        if (!attachments || attachments.length === 0) {
            return;
        }

        attachments.forEach(attachment => {
            let fileIcon = getFileIcon(attachment.name);
            const attachmentElement = `
            <div class="col-xxl-4 col-lg-6" data-attachment-id="${attachment.id}">
                <div class="border rounded border-dashed p-2">
                    <div class="d-flex align-items-center">
                        <div class="flex-shrink-0 me-3">
                            <div class="avatar-sm">
                                <div class="avatar-title bg-light text-secondary rounded fs-24">
                                    <i class="${fileIcon}"></i>
                                </div>
                            </div>
                        </div>
                        <div class="flex-grow-1 overflow-hidden">
                            <h5 class="fs-13 mb-1"><a href="#" class="text-body text-truncate d-block">${attachment.name}</a></h5>
                        </div>
                        <div class="flex-shrink-0 ms-2">
                            <div class="d-flex gap-1">
                                <button type="button" class="btn btn-icon text-muted btn-sm fs-18 download-attachment" 
                                        data-attachment-id="${attachment.id}">
                                    <i class="ri-download-2-line"></i>
                                </button>
                                <div class="dropdown">
                                    <button class="btn btn-icon text-muted btn-sm fs-18 dropdown" type="button" 
                                            data-bs-toggle="dropdown" aria-expanded="false">
                                        <i class="ri-more-fill"></i>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a class="dropdown-item delete-attachment" href="#" data-attachment-id="${attachment.id}">
                                                <i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Supprimer
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;

            resourcesRow.append(attachmentElement);
        });

        $(".download-attachment").on("click", function () {
            const attachmentId = $(this).data("attachment-id");
            window.location.href = `/taskAttachement/download/${attachmentId}`;
        });

        $(document).on("click", ".delete-attachment", function (e) {
            e.preventDefault();
            const attachmentId = $(this).data("attachment-id");

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to recover this attachment!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel"
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/taskattachement/delete/${attachmentId}`,
                        type: "DELETE",
                        success: function (response) {
                            $(`div[data-attachment-id="${attachmentId}"]`).remove();
                            Swal.fire("Deleted!", response.message, "success");
                        },
                        error: function () {
                            Swal.fire("Error!", "Failed to delete attachment.", "error");
                        }
                    });
                }
            });
        });
    }
    function getFileIcon(fileName) {
        const extension = fileName.split('.').pop().toLowerCase();

        const iconMap = {
            // Images
            'jpg': 'ri-image-line',
            'jpeg': 'ri-image-line',
            'png': 'ri-image-line',
            'gif': 'ri-image-line',
            'svg': 'ri-image-line',
            // Documents
            'pdf': 'ri-file-pdf-line',
            'doc': 'ri-file-word-line',
            'docx': 'ri-file-word-line',
            'xls': 'ri-file-excel-line',
            'xlsx': 'ri-file-excel-line',
            'ppt': 'ri-file-ppt-2-line',
            'pptx': 'ri-file-ppt-2-line',
            // Archives
            'zip': 'ri-file-zip-line',
            'rar': 'ri-file-zip-line',
            '7z': 'ri-file-zip-line',
            // Code
            'html': 'ri-code-line',
            'css': 'ri-code-line',
            'js': 'ri-code-line',
            'json': 'ri-code-line',
            'xml': 'ri-code-line',
            // Other formats
            'txt': 'ri-file-text-line',
            'csv': 'ri-file-list-line'
        };

        return iconMap[extension] || 'ri-file-line';
    }
});
