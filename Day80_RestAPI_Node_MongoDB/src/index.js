#!/user/bin/env node

import _ from "lodash";
import Hapi from "hapi";

import mongodb from "hapi-mongodb";

const server = new Hapi.Server({port:5333});
//server.connection({port:5333});
server.start(err =>{
    if(err)
        throw err;
    console.log("Server running at : ${server.info.uri}");
});