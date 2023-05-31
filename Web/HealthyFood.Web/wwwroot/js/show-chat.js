function openForm() {
    document.getElementById("chat").style.display = "block";
    document.getElementById("chat-btn").style.display = "none";
}

function closeForm() {
    document.getElementById("chat").style.display = "none";
    document.getElementById("chat-btn").style.display = "block";
}

$(window).on('scroll', function () {
    if ($(this).scrollTop() > 100) {
        if (document.getElementById("chat").style.display != "block") {
            $('#chat-btn').fadeIn();
        }
    } else {
        $('#chat-btn').fadeOut();
    }
});