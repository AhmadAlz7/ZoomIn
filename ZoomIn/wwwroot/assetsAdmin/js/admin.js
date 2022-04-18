
function htmlTableToExcel(tableId, fileName = '') {
    var downloadedFileName = 'excel_table_data';
    var TableDataType = 'application/vnd.ms-excel';
    var selectTable = document.getElementById(tableId);
    var htmlTable = selectTable.outerHTML.replace(/ /g, '%20');

    fileName = fileName ? fileName + '.xls' : downloadedFileName + '.xls';
    var downloadingURL = document.createElement("a");
    document.body.appendChild(downloadingURL);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', htmlTable], {
            type: TableDataType
        });
        navigator.msSaveOrOpenBlob(blob, fileName);
    } else {

        downloadingURL.href = 'data:' + TableDataType + ', ' + htmlTable;
        downloadingURL.download = fileName;
        downloadingURL.click();
    }
}