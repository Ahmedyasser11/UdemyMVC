$(document).ready(function () {
    $('.position-relative').hover(function () {
        $(this).find('label').show();
    }, function () {
        $(this).find('label').hide();
    });

    $('#ImageUpload').change(function () {
        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#profilePicture').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    });
});