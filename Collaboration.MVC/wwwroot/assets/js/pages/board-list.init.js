
var favouriteBtn = document.querySelectorAll(".favourite-btn");
if (favouriteBtn) {
    Array.from(document.querySelectorAll(".favourite-btn")).forEach(function (item) {
        item.addEventListener("click", function (event) {
            this.classList.toggle("active");
        });
    });
}

// Remove product from cart
var removeProduct = document.getElementById('removeProjectModal')
if (removeProduct) {
    removeProduct.addEventListener('show.bs.modal', function (e) {
        document.getElementById('remove-project').addEventListener('click', function (event) {
            e.relatedTarget.closest('.project-card').remove();
            document.getElementById("close-modal").click();
        });
    });
}

$(document).ready(() => {
    $('#createBoardForm').on('submit', async (e) => {
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
            $('#createBoardForm')[0].reset();
            loadBoards();
            $('#createboardModal').modal('hide');
        } catch (error) {
            console.error('Error:', error);
            alert('Failed to create board.');
        }
    });

    $(document).ready(() => {
        loadBoards();
    });

    async function loadBoards() {
        try {
            const response = await $.ajax({
                url: '/Board/Get',
                type: 'GET',
                contentType: 'application/json'
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
});
