$(function () {
    // preload audio
    var toast = new Audio('media/toast.wav');
    $('.code').on('click', function (e) {
        e.preventDefault();
        alert($(this).data('code'));
        $('#code').text($(this).data('code'));
        $('#product').text($(this).data('product'));
        // first pause the audio (in case it is still playing)
        toast.pause();
        // reset the audio
        toast.currentTime = 0;
        // play audio
        toast.play();
        $('#toast').toast({ autohide: false }).toast('show');
    });

});