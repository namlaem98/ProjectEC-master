var common = {
    init: function () {
        common.registerEvent();
    },

    registerEvent: function () {
        $('#username').change(function (e) {
            $.ajax({
                url: '/Admin/Account/CheckUsername',
                dataType: 'json',
                data: {
                    username: $(this).val()
                },
                type: 'POST',
                success: function (res) {
                    if (res.status == false) {
                        document.getElementById("UsernameMessage").innerHTML = "Tài khoản đã tồn tại";
                        $('#username').val("");
                        $('#username').focus();
                    }
                    else {
                        document.getElementById("UsernameMessage").innerHTML = "";
                    }
                }
            });
        })
    }
}

common.init();