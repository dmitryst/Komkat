$(function () {

    $('#ModelsDivId').hide();
    $('#SubmitId').hide();

    $('#BrandId').change(function () {
        var URL = $('#BrandModelFormId').data('modelListAction');
        $.getJSON(URL + '/' + $('#BrandId').val(), function (data) {
            var items = '<option>Select a Brand</option>';
            $.each(data, function (i, brand) {
                items += "<option value='" + brand.Value + "'>" + brand.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#ModelsId').html(items);
            $('#ModelsDivId').show();

        });
    });

    $('#ModelsId').change(function () {
        $('#SubmitId').show();
    });
});