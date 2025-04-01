$(document).ready(() => {
    console.log($("#user-claims").val());

    loadBoards();

    loadUsers();

    $('#btnCreateBoard').on('click', function () {
        table.ajax.reload();    
    });

    $("#showSelectedOnly").on("change", function () {
        if (this.checked) {
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                const row = table.row(dataIndex).node();
                const checkbox = $(row).find(".task-checkbox");
                return checkbox.is(":checked");
            });
        } else {
            $.fn.dataTable.ext.search.pop();
        }
        table.draw();
    });

});

var favouriteBtn = document.querySelectorAll(".favourite-btn");

if (favouriteBtn) {
    Array.from(document.querySelectorAll(".favourite-btn")).forEach(function (item) {
        item.addEventListener("click", function (event) {
            this.classList.toggle("active");
        });
    });
}

var removeProduct = document.getElementById('removeProjectModal')
if (removeProduct) {
    removeProduct.addEventListener('show.bs.modal', function (e) {
        document.getElementById('remove-project').addEventListener('click', function (event) {
            e.relatedTarget.closest('.project-card').remove();
            document.getElementById("close-modal").click();
        });
    });
}

const boardsPerPage = 8;

let currentPage = 1;

let allBoards = [];

const table = $("#tasksTableReference").DataTable({
    ajax: {
        url: `/task/GetAllReferences`,
        type: "GET",
        dataSrc: ""
    },
    columns: [
        {
            data: null,
            orderable: false,
            className: "text-center",
            render: function (data, type, row) {
                return `<input type="checkbox" class="task-checkbox" data-id="${row.id}">`;
            }
        },
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
        }
    ],
    autoWidth: false,
    columnDefs: [
        { targets: 0, orderable: false, width: "50px", className: "text-center" },
        { width: "50px", targets: 1 },
        { width: "150px", targets: 2 },
        { width: "120px", targets: 3 },
        { width: "100px", targets: 4 },
        { width: "100px", targets: 5 },
        { width: "120px", targets: 6 },
        { width: "100px", targets: 7 }
    ],
    fixedColumns: true
});

const loadBoards = async (selectedUserIds = []) => {
    try {
        if (!Array.isArray(selectedUserIds) || selectedUserIds.length === 0) {
            selectedUserIds = null;
        }

        const response = await $.ajax({
            url: '/Board/Get',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(selectedUserIds)
        });

        allBoards = response;
        renderBoards();

    } catch (error) {
        console.error('Error:', error);
        alert('Failed to load boards.');
    }
};

const renderBoards = () => {
    $('#boardRow').empty();

    const start = (currentPage - 1) * boardsPerPage;
    const end = start + boardsPerPage;
    const paginatedBoards = allBoards.slice(start, end);

    if (paginatedBoards.length === 0) {
        $('#boardRow').html('<p class="text-center">No boards found.</p>');
    } else {
        paginatedBoards.forEach(board => {
            var boardHtml = `
                <div class="col-xxl-3 col-sm-6 project-card">
                    <div class="card card-height-100">
                        <div class="card-body">
                            <div class="d-flex flex-column h-100">
                                <div class="d-flex">
                                    <div class="flex-grow-1">
                                        <div class="badge bg-warning-subtle text-warning fs-12">${board.status}</div>
                                    </div>
                                    <div class="flex-shrink-0">
                                        <div class="d-flex gap-1 align-items-center">
                                            <button type="button" class="btn avatar-xs mt-n1 p-0 favourite-btn">
                                                <span class="avatar-title bg-transparent fs-15">
                                                    <i class="ri-star-fill"></i>
                                                </span>
                                            </button>
                                            <div class="dropdown">
                                                <button class="btn btn-link text-muted p-1 mt-n2 py-0 text-decoration-none fs-15" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-more-horizontal icon-sm">
                                                        <circle cx="12" cy="12" r="1"></circle>
                                                        <circle cx="19" cy="12" r="1"></circle>
                                                        <circle cx="5" cy="12" r="1"></circle>
                                                    </svg>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-end">
                                                    <a class="dropdown-item" href="Task/index/${board.id}">
                                                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i> View
                                                    </a>
                                                    <a class="dropdown-item" href="Task/index/${board.id}">
                                                        <i class="ri-delete-bin-3-fill align-bottom me-2 text-muted"></i> Delete
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="d-flex mb-2">
                                    <div class="flex-shrink-0 me-3">
                                        <div class="avatar-sm">
                                            <span class="avatar-title bg-warning-subtle rounded p-2">
                                                <img src="assets/images/brands/slack.png" alt="" class="img-fluid p-1">
                                            </span>
                                        </div>
                                    </div>
                                    <div class="flex-grow-1">
                                        <h5 class="mb-1 fs-14"><a href="apps-projects-overview.html" class="text-body">${board.title}</a></h5>
                                        <p class="text-muted text-truncate-two-lines mb-3">${board.discription}</p>
                                    </div>
                                </div>
                                <div class="mt-auto">
                                    <div class="d-flex mb-2">
                                        <div class="flex-grow-1">
                                            <div>Tasks</div>
                                        </div>
                                        <div class="flex-shrink-0">
                                            <div><i class="ri-list-check align-bottom me-1 text-muted"></i>
                                                ${board.completedTask}/${board.taskCount}</div>
                                        </div>
                                    </div>
                                    <div class="progress progress-sm animated-progress">
                                        <div class="progress-bar bg-success" role="progressbar" aria-valuenow="${board.taskProgress}" aria-valuemin="0" aria-valuemax="100" style="width: ${board.taskProgress}%;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer bg-transparent border-top-dashed py-2">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1">
                                    <div class="avatar-group" id="avatar-group-${board.id}">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            $('#boardRow').append(boardHtml);

            board.boardTeam.forEach(member => {
                const firstChar = member.fullName.charAt(0).toUpperCase();
                const avatarHtml = `
                    <a href="javascript:void(0);" class="avatar-group-item" data-bs-toggle="tooltip" title="${member.fullName}">
                        <div class="avatar-xxs">
                            <div class="avatar-title rounded-circle bg-danger">
                                ${firstChar}
                            </div>
                        </div>
                    </a>
                `;
                $(`#avatar-group-${board.id}`).append(avatarHtml);
            });
        });
    }

    renderPagination();
};

const renderPagination = () => {
    const totalPages = Math.ceil(allBoards.length / boardsPerPage);
    const paginationContainer = $('#boardPagination');
    paginationContainer.empty();

    if (totalPages <= 1) return;

    let paginationHtml = `
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="changePage(${currentPage - 1})">Previous</a>
        </li>
    `;

    for (let i = 1; i <= totalPages; i++) {
        paginationHtml += `
            <li class="page-item ${i === currentPage ? 'active' : ''}">
                <a class="page-link" href="#" onclick="changePage(${i})">${i}</a>
            </li>
        `;
    }

    paginationHtml += `
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="changePage(${currentPage + 1})">Next</a>
        </li>
    `;

    paginationContainer.append(paginationHtml);
};

const changePage = (page) => {
    if (page < 1 || page > Math.ceil(allBoards.length / boardsPerPage)) return;
    currentPage = page;
    renderBoards();
};

const loadUsers = async () => {
    try {
        const response = await $.ajax({
            url: '/User/Get',
            type: 'GET',
            contentType: 'application/json'
        });

        if (window.choicesInstance) {
            window.choicesInstance.destroy();
        }

        const userSelect = document.getElementById('userList');
        const placeholderText = userSelect.getAttribute('data-placeholder') || "Assigné à";

        window.choicesInstance = new Choices(userSelect, {
            removeItemButton: true,
            placeholder: true,
            placeholderValue: placeholderText,
            searchPlaceholderValue: 'Type to search...'
        });

        window.choicesInstance.setChoices(
            response.map(user => ({
                value: user.id,
                label: user.fullName
            })),
            'value',
            'label',
            true
        );

    } catch (error) {
        console.error('Error:', error);
        alert('Failed to load users.');
    }
}

const handleCreateBoard = async (e) => {

    e.preventDefault();

    const selectedTaskIds = table.$(".task-checkbox:checked").map(function () {
        return $(this).data("id");
    }).get();

    const data = {
        title: $('#boardTitle').val(),
        description: $('#boardDescription').val(),
        tasksId: selectedTaskIds
    };

    console.log(data);

    try {
        const response = await $.ajax({
            url: '/Board/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        });

        loadBoards();
        $('#createBoardForm')[0].reset();
        $('#createboardModal').modal('hide');
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to create board.');
    }
};

$('#createBoardForm').on('submit', handleCreateBoard);

document.getElementById('userList').addEventListener('change', () => {
    const selectedIds = Array.from(document.getElementById('userList').selectedOptions)
        .map(option => option.value);

    loadBoards([...new Set(selectedIds)]);
});
function addCheckboxListeners() {
    const selectAll = document.getElementById("selectAll");
    const checkboxes = document.querySelectorAll(".row-checkbox");

    if (selectAll) {
        selectAll.addEventListener("change", () => {
            checkboxes.forEach(cb => cb.checked = selectAll.checked);
        });
    }
}
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