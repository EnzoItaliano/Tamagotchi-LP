import "reflect-metadata";
import {createConnection} from "typeorm";
import * as express from "express";
import * as bodyParser from "body-parser";
import {Request, Response} from "express";
import * as account from "./routes/Account";
import * as pet from "./routes/Pet";

createConnection({
    type: "mysql",
    host: "localhost",
    port: 3306,
    username: "test",
    password: "test",
    database: "tamagotchi",
    entities: [
        __dirname + "/entity/*.ts"
    ],
    synchronize: true,
    logging: false,
    dropSchema: true
}).then(async connection => {

    // create express app
    const app = express();
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({ extended: true }));

    // register express routes from defined application routes
    // var account = require("./routes");
    app.use('/user', account);
    app.use('/pet', pet);

    // setup express app here
    // ...

    // start express server
    app.listen(3000);

    console.log("Express server has started on port 3000. Open http://localhost:3000/user to see results");

}).catch(error => console.log(error));
