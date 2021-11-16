$(document).ready(function () {
    loadposts();

    $("#thepost").click(function () {
        var data = $('#caixa_postagem').val();
        $.ajax({
            type: 'POST',
            url: '/api/Postar',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            success: function (resp) {
                $("#caixa_postagem").val('');
                loadposts();
            }
        });
    });

    $(document).on('click', '.del', function (e) {
        var id = $(this).parent().parent().attr('id');
        e.preventDefault();

        switch (window.location.pathname.replace('/', '').toLowerCase()){
            case 'perfil':
                $.ajax({
                    type: 'DELETE',
                    url: '/api/Postar',
                    data: JSON.stringify(id),
                    contentType: 'application/json',
                    dataType: 'text',
                    success: function (resp) {
                        loadposts();
                    }
                });
                break;
            default:
                break;
        }


    });
});

function loadposts() {
    switch (window.location.pathname.replace('/', '').toLowerCase()) {
        case 'perfil':
            $.get('/api/Postar/2', function (posts) {
                $("#feedbox").empty();
                posts = posts.reverse();

                posts.forEach((item) => {
                    var id = item.id;
                    var nome = item.usuario.nome;
                    var sobrenome = item.usuario.sobrenome;
                    var mensagem = item.mensagem;
                    var datapostagem = new Date(item.dataPostagem);

                    $("#feedbox").prepend(
                        `<div id="${id}" class="container-fluid post-box Muli">
                             <div style="text-align: end;">
                                <a class="del">
                                    <div class="del-button"></div>
                                </a>
                            </div>
                            <div class="user-infos">
                                <div class="user-picture-min"></div>
                                <div class="post-text post-font">
                                    <div>
                                        <a href="#">${nome} ${sobrenome}</a>
                                        <div style="font-size: small; color: #ffffff85;">
                                            ${datapostagem.getDate()}/${datapostagem.getMonth() + 1}/${datapostagem.getFullYear()} às ${datapostagem.getHours()}:${(datapostagem.getMinutes() < 10 ? '0' : '') + datapostagem.getMinutes()}
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="post-text">
                                ${mensagem}
                            </div>

                            <div class="container-fluid post-btn-area">
                                <button style="margin: 3px;" class="post-btn"><div class="like-btn"></div></button>
                                <button style="margin: 3px;" class="post-btn"><div class="dislike-btn"></div></button>
                            </div>
                        </div>`
                    );
                });
            });
            break;
        default:
            $.get('/api/Postar/1', function (posts) {
                $("#feedbox").empty();
                posts = posts.reverse();

                posts.forEach((item) => {
                    var id = item.id;
                    var nome = item.usuario.nome;
                    var sobrenome = item.usuario.sobrenome;
                    var mensagem = item.mensagem;
                    var datapostagem = new Date(item.dataPostagem);

                    $("#feedbox").prepend(
                        `<div id="${id}" class="container-fluid post-box Muli">
                             <div style="text-align: end;">
                            </div>
                            <div class="user-infos">
                                <div class="user-picture-min"></div>
                                <div class="post-text post-font">
                                    <div>
                                        <a href="#">${nome} ${sobrenome}</a>
                                        <div style="font-size: small; color: #ffffff85;">
                                            ${datapostagem.getDate()}/${datapostagem.getMonth() + 1}/${datapostagem.getFullYear()} às ${datapostagem.getHours()}:${(datapostagem.getMinutes() < 10 ? '0' : '') + datapostagem.getMinutes()}
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="post-text">
                                ${mensagem}
                            </div>

                            <div class="container-fluid post-btn-area">
                                <button style="margin: 3px;" class="post-btn"><div class="like-btn"></div></button>
                                <button style="margin: 3px;" class="post-btn"><div class="dislike-btn"></div></button>
                            </div>
                        </div>`
                    );
                });
            });
            break;
    }
}