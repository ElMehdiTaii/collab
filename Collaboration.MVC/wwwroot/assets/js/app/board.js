$(document).ready(() => {
    loadBoards();
    loadUsers();
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

const loadBoards = async (selectedUserIds = []) => {
    try {
        if (!Array.isArray(selectedUserIds) || selectedUserIds.length === 0) {
            selectedUserIds = null;
        }
        const response = await $.ajax({
            url: '/Board/Get',
            type: 'POST',
            contentType: 'application/json',
            data:  JSON.stringify(selectedUserIds)
        });

        $('#boardList').empty();

        if ($('#boardList').children('.row').length === 0) {
            $('#boardList').append('<div class="row" id="boardRow"></div>');
        }

        response.forEach(board => {
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
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-more-horizontal icon-sm"><circle cx="12" cy="12" r="1"></circle><circle cx="19" cy="12" r="1"></circle><circle cx="5" cy="12" r="1"></circle></svg>
                                                        </button>

                                                        <div class="dropdown-menu dropdown-menu-end" style="">
                                                            <a class="dropdown-item" href="Task/index/${board.id}"><i class="ri-eye-fill align-bottom me-2 text-muted"></i>
                                                                View</a>
                                                            <!--<a class="dropdown-item" href="apps-projects-create.html"><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
                                                                Edit</a>
                                                            <div class="dropdown-divider"></div>
                                                            <a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#removeProjectModal"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i>
                                                                Remove</a>-->
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
                                                <div class="progress-bar bg-success" role="progressbar" aria-valuenow="34" aria-valuemin="0" aria-valuemax="100" style="width: ${board.taskProgress}%;">
                                                </div><!-- /.progress-bar -->
                                            </div><!-- /.progress -->
                                        </div>
                                    </div>

                                </div>
                                <!-- end card body -->
                                <div class="card-footer bg-transparent border-top-dashed py-2">
                                    <div class="d-flex align-items-center">
                                        <div class="flex-grow-1">
                                             <div class="avatar-group" id="avatar-group-${board.id}">
                                        </div>
                                        </div>
                                        <div class="flex-shrink-0">
                                            <div class="text-muted">
                                                <i class="me-1 align-bottom"></i>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <!-- end card footer -->
                            </div>
                            <!-- end card -->
                        </div>
    `;

            $('#boardRow').append(boardHtml);

            board.boardTeam.forEach(member => {

                const firstChar = member.fullName.charAt(0).toUpperCase();

                const avatarHtml = `
            <a href="javascript: void(0);" class="avatar-group-item"
               data-bs-toggle="tooltip" data-bs-trigger="hover"
               data-bs-placement="top" title="${member.fullName}">
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

    } catch (error) {
        console.error('Error:', error);
        alert('Failed to load boards.');
    }
}

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

    const data = {
        title: $('#boardTitle').val(),
        description: $('#boardDescription').val()
    };

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

const loadTasks = async () => {
    try {
        const response = await $.ajax({
            url: '/Task/GetAll',
            type: 'GET',
            contentType: 'application/json'
        });

        renderTaskTable(response);

    } catch (error) {
        console.error('Error:', error);
        alert('Failed to load Tasks.');
    }
};

$('#btnCreateBoard').on('click', loadTasks);

document.getElementById('userList').addEventListener('change', () => {
    const selectedIds = Array.from(document.getElementById('userList').selectedOptions)
        .map(option => option.value);

    loadBoards([...new Set(selectedIds)]);
});
function renderTaskTable(tasks) {
    const tableBody = document.getElementById("tableBody");
    tableBody.innerHTML = "";

    tasks.forEach((task, index) => {
        const row = `
                    <tr>
                        <td><input type="checkbox" class="row-checkbox"></td>
                        <td>${task.title}</td>
                        <td>${task.description}</td>
                        <td>${task.status ?? "N/A"}</td>
                        <td>${task.priority ?? "N/A"}</td>
                        <td>${task.startDate ? new Date(task.startDate).toLocaleDateString() : "N/A"}</td>
                        <td>${task.endDate ? new Date(task.endDate).toLocaleDateString() : "N/A"}</td>
                        <td></td>
                    </tr>
                `; //${task.assignedTo}
        tableBody.innerHTML += row;
    });

    addCheckboxListeners();
}
function addCheckboxListeners() {
    const selectAll = document.getElementById("selectAll");
    const checkboxes = document.querySelectorAll(".row-checkbox");

    if (selectAll) {
        selectAll.addEventListener("change", () => {
            checkboxes.forEach(cb => cb.checked = selectAll.checked);
        });
    }
}

document.getElementById("searchInput").addEventListener("keyup", function () {
    let filter = this.value.toLowerCase();
    let rows = document.querySelectorAll("#tableBody tr");

    rows.forEach(row => {
        let text = row.textContent.toLowerCase();
        row.style.display = text.includes(filter) ? "" : "none";
    });
});

document.getElementById("showSelectedOnly").addEventListener("change", function () {
    let checkboxes = document.querySelectorAll(".row-checkbox");
    checkboxes.forEach(cb => {
        let row = cb.closest("tr");
        if (this.checked) {
            row.style.display = cb.checked ? "" : "none";
        } else {
            row.style.display = "";
        }
    });
});