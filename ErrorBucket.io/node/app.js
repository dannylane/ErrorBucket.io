var http = require('http');

http.createServer(function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/plain' });
    res.end('I am listening on ' + process.env.PORT);
}).listen(process.env.PORT);