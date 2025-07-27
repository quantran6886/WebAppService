function tinymceSettings(selectors ,height) {
    selectors.forEach(selector => {
        tinymce.init({
            selector: selector,
            plugins: 'preview importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media codesample table charmap pagebreak nonbreaking anchor insertdatetime advlist lists wordcount help charmap quickbars emoticons accordion',
            editimage_cors_hosts: ['picsum.photos'],
            menubar: 'file edit view insert format tools table help',
            toolbar: "undo redo | accordion accordionremove | blocks fontfamily fontsize | bold italic underline strikethrough | align numlist bullist | link image | table media | lineheight outdent indent| forecolor backcolor removeformat | charmap emoticons | code fullscreen preview | save print | pagebreak anchor codesample | ltr rtl",
            autosave_ask_before_unload: true,
            autosave_interval: '30s',
            autosave_prefix: '{path}{query}-{id}-',
            autosave_restore_when_empty: false,
            autosave_retention: '2m',
            image_dimensions: true,
            image_advtab: true,
            extended_valid_elements: 'img[class|src|alt|title|width|height|style]',
            language: 'vi',
            importcss_append: true,
            content_style: "body { font-family: 'Inter', sans-serif; }",
            content_css: [
                'https://fonts.googleapis.com/css2?family=Montserrat&display=swap',
                'https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;500;600;700&display=swap',
                'https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap'
            ],
            font_family_formats: `
                Montserrat=Montserrat, sans-serif;
                Plus Jakarta Sans='Plus Jakarta Sans', sans-serif;
                Inter=Inter, sans-serif;
                Noto Sans='Noto Sans', sans-serif;
                Manrope=Manrope, sans-serif;
                Arial=arial,helvetica,sans-serif;
                Courier New=courier new,courier,monospace;
                Georgia=georgia,palatino;
                Tahoma=tahoma,arial,helvetica,sans-serif;
                Times New Roman=times new roman,times;
                Verdana=verdana,geneva;
              `,
            file_picker_callback: (callback, value, meta) => {
                let input = document.createElement('input');

                if (meta.filetype === 'image') {
                    input.setAttribute('type', 'file');
                    input.setAttribute('accept', 'image/*');

                    input.onchange = function () {
                        var file = this.files[0];
                        var reader = new FileReader();

                        reader.onload = function () {
                            callback(reader.result, { alt: file.name });
                        };
                        reader.readAsDataURL(file);
                    };

                    input.click();
                }
                else if (meta.filetype === 'media') {
                    input.setAttribute('type', 'file');
                    input.setAttribute('accept', 'video/*');

                    input.onchange = function () {
                        var file = this.files[0];

                        if (file) {
                            var videoUrl = URL.createObjectURL(file);
                            callback(videoUrl, { source2: file.name });
                        }
                    };

                    input.click();
                }
            },
            height: height,
            image_caption: true,
            quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
            noneditable_class: 'mceNonEditable',
            toolbar_mode: 'sliding',
            contextmenu: 'link image table',
        });
    });
}
