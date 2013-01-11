﻿var currentHandler = window.onerror;
var cachedErrrors = [];
var count = 0;
var src = 'http://localhost/ErrorBucket/log?error=';
__
window.onerror = function (errorMsg, url, lineNumber) {
    var messageToLog = errorMsg + '&url=' + url + '&lineNumber=' + lineNumber;
    cachedErrrors.push(messageToLog);

    var bodyEle = document.getElementsByTagName('body')[0];
    if (bodyEle) {
        while (cachedErrrors.length > 0) {
            var imgElement = document.createElement('img');
            imgElement.setAttribute('id', 'error-bucket-image-' + count);
            imgElement.setAttribute('src', src + cachedErrrors.pop());
            bodyEle.appendChild(imgElement);
            count++;
        }
    }

    if (currentHandler) {
        return currentHandler(errorMsg, url, lineNumber);
    }
    return false;
}