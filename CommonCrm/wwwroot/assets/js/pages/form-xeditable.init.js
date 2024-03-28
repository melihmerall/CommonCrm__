/*
Template Name: Nazox -  Admin & Dashboard Template
Author: Themesdesign
Contact: themesdesign.in@gmail.com
File: Form-Xeditable Js File
*/

$(function () {

    //modify buttons style
    $.fn.editableform.buttons =
        '<button type="submit" class="btn btn-success editable-submit btn-sm waves-effect waves-light"><i class="mdi mdi-check"></i></button>' +
        '<button type="button" class="btn btn-danger editable-cancel btn-sm waves-effect waves-light"><i class="mdi mdi-close"></i></button>';

    //inline

    $('#inline-username').editable({
        type: 'text',
        pk: 1,
        name: 'username',
        title: 'Enter username',
        mode: 'inline',
        inputclass: 'form-control-sm'
    });
    $('#inline-dolar').editable({
        type: 'text',
        id: 'editable-dolar',
        pk: 1,
        name: 'currencyDolar',
        title: 'currency',
        mode: 'inline',
        inputclass: 'form-control-sm',
        success: function (response, newValue) {

            debugger;
            $.ajax({
                url: '/Product/CurrencyChange',
                type: 'POST',
                data: {
                    newValue: parseFloat(newValue.replace("$", "").replace(",", ".")),
                    code: "dolar",
                },
                success: function (response) {

                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: "Başarılı",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    setTimeout(function(){
                        location.reload();
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    // Hata durumunda yapılacak işlemler
                    console.error('Hata oluştu:', error);
                    Swal.fire({
                        title: 'Hata!',
                        text: 'Bir hata oluştu: ' + error,
                        icon: 'error'
                    });
                }
            });
            $('#inline-dolar').val(newValue);
            $('#hidden-dolar-currency').val(newValue);

        }
    });
    $('#inline-euro').editable({
        type: 'text',
        pk: 1,
        id: 'editable-euro',
        name: 'currencyEuro',
        title: 'currency',
        mode: 'inline',
        inputclass: 'form-control-sm',
        success: function (response, newValue) {

            $.ajax({
                url: '/Product/CurrencyChange',
                type: 'POST',
                data: {
                    newValue: parseFloat(newValue.replace("€", "").replace(",", ".")),
                    code: "euro",
                },
                success: function (response) {

                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: "Başarılı",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    setTimeout(function(){
                        location.reload();
                    }, 2000);
                },
                error: function (xhr, status, error) {
                    // Hata durumunda yapılacak işlemler
                    console.error('Hata oluştu:', error);
                    Swal.fire({
                        title: 'Hata!',
                        text: 'Bir hata oluştu: ' + error,
                        icon: 'error'
                    });
                }
            });

            $('#inline-euro').val(newValue);
            $('#hidden-euro-currency').val(newValue);
        }
    });
    $('#inline-firstname').editable({
        validate: function (value) {
            if ($.trim(value) == '') return 'This field is required';
        },
        mode: 'inline',
        inputclass: 'form-control-sm'
    });

    $('#inline-sex').editable({
        prepend: "not selected",
        mode: 'inline',
        inputclass: 'form-control-sm',
        source: [
            {value: 1, text: 'Male'},
            {value: 2, text: 'Female'}
        ],
        display: function (value, sourceData) {
            var colors = {"": "#98a6ad", 1: "#5fbeaa", 2: "#5d9cec"},
                elem = $.grep(sourceData, function (o) {
                    return o.value == value;
                });

            if (elem.length) {
                $(this).text(elem[0].text).css("color", colors[value]);
            } else {
                $(this).empty();
            }
        }
    });

    $('#inline-status').editable({
        mode: 'inline',
        inputclass: 'form-control-sm'
    });

    $('#inline-group').editable({
        showbuttons: false,
        mode: 'inline',
        inputclass: 'form-control-sm'
    });

    $('#inline-dob').editable({
        mode: 'inline',
        inputclass: 'form-control-sm'
    });

    $('#inline-comments').editable({
        showbuttons: 'bottom',
        mode: 'inline',
        inputclass: 'form-control-sm'
    });

});