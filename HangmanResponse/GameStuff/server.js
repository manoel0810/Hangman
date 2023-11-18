const http = require('http');
const finalhandler = require('finalhandler');
const serveStatic = require('serve-static');
const cors = require('cors');

// Define a pasta de onde os arquivos serão servidos
const serve = serveStatic('.', { 'index': ['index.html'] });

// Configura o middleware CORS
const corsMiddleware = cors();

// Cria o servidor HTTP
const server = http.createServer((req, res) => {
    const done = finalhandler(req, res);
    corsMiddleware(req, res, err => {
        if (err) return done(err);
        serve(req, res, done);
    });
});

// Inicia o servidor na porta desejada (por exemplo, 8080)
server.listen(8080, () => {
    console.log('Servidor HTTP iniciado na porta 8080');
});