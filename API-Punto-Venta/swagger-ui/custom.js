window.addEventListener("load", function () {
        customizeSwaggerUI();
    });
function customizeSwaggerUI() {
    setTimeout(function () {        
        var tag = '<rapi-pdf style="display:none" id="thedoc"> </rapi-pdf>';
        var btn = '<button id="btn" style="font-size:16px;padding: 6px 16px;text-align: center;white-space: nowrap;background-color: orangered;color: white;border: 0px solid #333;cursor: pointer;" type="button" onclick="downloadPDF()">Download API Document </button>';
        var oldhtml = document.getElementsByClassName('info')[0].innerHTML;
        document.getElementsByClassName('info')[0].innerHTML = oldhtml + '</br>' + tag + '</br>' + btn;
    }, 1200);
}
function downloadPDF() {
    var client = new XMLHttpRequest();
    client.overrideMimeType("application/json");
    client.open('GET', 'v1/swagger.json');
    var jsonAPI = "";
    client.onreadystatechange = function () {
        if (client.responseText != 'undefined' && client.responseText != "") {
            jsonAPI = client.responseText;
            if (jsonAPI != "") {
                let docEl = document.getElementById("thedoc");
                var key = jsonAPI.replace('\"Authorization: Bearer {token}\"', "");
                let objSpec = JSON.parse(key);
                docEl.generatePdf(objSpec);
            }
        }
    }
    client.send();   
}
