#!/usr/bin/env node

import _ from 'lodash';
import Hapi from 'hapi';
import good from 'good';
import inert from 'inert';
import vision from 'vision';
import hapiSwagger from 'hapi-swagger';
import colors from 'colors';

import mongodb from 'hapi-mongodb';
import dbconfig from '../dbconfig.json';
import Boom from 'boom';

const server = new Hapi.Server();
server.connection({port:5334});

server.route( {
    method: 'GET',
    path: '/help',
    handler: (request, reply) => {
        let helps = [];
        _.forEach(server.table()[0].table, o => {
            helps.push(`${o.path}`.underline.green.inverse + `\n` + ` ${o.settings.description}`);
        });
        reply(helps);
    },
    config: {
        description: '현재 보고 있는 도움말이지 말입니다.',
        notes: '도움말',
        tags:['api']
    }
});

server.route( {
    method: 'GET',
    path: '/user/{email*}',
    handler: (request, reply) => {
        let db = request.server.plugins['hapi-mongodb'].db;
        db.collection('users').findOne({'email': request.params.email}, (err, ret) => {     //request.params.email params안에 만들어짐
            if (err)
                return reply(Boom.internal('Internal Database Error', err));
            if (ret == null)
                return reply(Boom.notFound('Not found'));
            let user = {
                email: ret.email,
                userName: ret.userName,
                nickName: ret.nickName
            };
            reply(user);    //json 데이터를 리턴해줌
        });
    },
    config: {
        description: '특정 유저 정보를 보냅니다.',
        notes: '유저',
        tags:['api']
    }
});

server.route( {
    method: 'GET',
    path: '/users',
    handler: (request, reply) => {
        let db = request.server.plugins['hapi-mongodb'].db;
        db.collection('users').find().toArray((err, ret) => {
            if (err)
                return reply(Boom.internal('Internal Database Error', err));
            let users = [];
            for (let i in ret) {
                let user = {
                    email: ret[i].email,
                    userName: ret[i].userName,
                    nickName: ret[i].nickName
                };
                users.push(user);
            }
            reply(users);
        });
    },
    config: {
        description: '모든 유저 정보를 보냅니다.',
        notes: '유저',
        tags:['api']
    }
});

server.route( {
    method: 'POST',
    path: '/user/add',
    handler: (request, reply) => {
        let db = request.server.plugins['hapi-mongodb'].db;
        db.collection('users').findOne({'email':request.payload.email}, (err, ret) => {
            if (err)
                return reply(Boom.internal('Internal Database Error', err));
            if (ret)
                return reply(Boom.conflict('Duplicated Resource Error', err));
            let user = {
                email: request.payload.email,       // string 이기만 해도 됨
                userName: request.payload.userName, // 따옴표 쓰던가 이렇게 쓰던가 같음 Key : Value,
                nickName: request.payload.nickName
            };
            db.collection('users').insert(user, {w:1}, err => {
                if (err)
                    return reply(Boom.internal('Internal Database Error', err));
                else
                    reply({AddSuccess: true});
            });
        });
    },
    config: {
        description: '유저를 추가합니다.',
        notes: '유저',
        tags:['api']
    }
});

server.route( {
    method: 'POST', // 'PATCH', not supported in Unity! //원래는 PATCH로 해야함
    path: '/user/update',
    handler: (request, reply) => {  //request = --data에 있는 값    //reply 는 callback 종료됨을 알림?
        let user = {
            email: request.payload.email,
            userName: request.payload.userName,
            nickName: request.payload.nickName
        };

        let db = request.server.plugins['hapi-mongodb'].db;
        db.collection('users').update({'email': request.payload.email}, user, (err, ret) => {
            if (err)
                return reply(Boom.internal('Internal Database error', err));    //에러 날려줌 예시 not found 404
            reply({UpdateSuccess : true});
        });
    },
    config: {
        description: '특정 유저 정보를 갱신합니다.',
        notes: '유저',
        tags:['api']
    }
});

server.route( {
    method: 'POST', // 'DELETE', , not supported in Unity!
    path: '/user/remove',
    handler: (request, reply) => {
        let db = request.server.plugins['hapi-mongodb'].db;
        db.collection('users').remove({'email': request.payload.email}, (err, ret) => {
            if (err)
                return reply(Boom.internal('Internal Database error', err));
            reply({RemoveSuccess : true});
        });
    },
    config: {
        description: '특정 유저 정보를 삭제합니다.',
        notes: '유저',
        tags:['api']
    }
});

// https://egghead.io/forums/lesson-discussion/topics/hapi-js-logging-with-good-and-good-console
let goodOptions = {
    reporters: {
        console: [
            {
                module: 'good-squeeze',
                name: 'Squeeze',
                args: [{
                    log: ['error', 'warn'],
                    response: '*'
                }]
            }, 
            {
                module: 'good-console',
                args: [{ log: '*', response: '*' }]
            },
            'stdout'
        ]
    }
}

// https://objectpartners.com/2016/11/16/adding-logging-and-swagger-to-a-hapi-node-server/
server.register(
[ 
    {
        register: good,
        options: goodOptions
    }, 
    {
        register: inert
    },
    {
        register: vision
    },
    {
        register: hapiSwagger,
        options: {
            info: {
                'title': 'API Documentation',
                'version': '1.0.0'
            }
        }
    },
    {
        register: mongodb,
        options: dbconfig
    }
], err => {
    if (err) {
        throw err; // something bad happened loading the plugin
    }
    server.start( err => {

        if (err) {
            throw err;
        }
        console.log('Server started !');
        server.log('info', 'Server running at: ' + server.info.uri);

        // mainLoop.addTask(arbitor);
        // mainLoop.start();
    });
});