$(window).on('scroll', function () {
    if ($(this).scrollTop() > 100) {
        if (document.getElementById("chat").style.display != "block") {
            $('#chat-btn').fadeIn();
        }
    } else {
        $('#chat-btn').fadeOut();
    }
});

$("#chat-btn").click(function () {
    const username = $("#userUsername").val();
});

$("#sendButton").click(function () {
    document.getElementById('messageInput').value = '';
});

// TODO: Figure out how to connect this with ChatGPT service