CKEDITOR.plugins.add('totubevideo', {
    icons: 'totubevideo',
    init: function (editor)
    {
        editor.addCommand('insertTotubevideo', {
            exec: function (editor)
            {
                BoardCA.Tools_InsertVideoClick();
            }
        });
        editor.ui.addButton('Totubevideo', {
            label: "영상 추가",
            command: 'insertTotubevideo',
            toolbar: 'insert'
        });
    }
});