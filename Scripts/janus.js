/* Janus WebRTC Gateway JavaScript Library - janus.js */
/*
 * Janus WebRTC Gateway (https://janus.conf.meetecho.com)
 * Janus JavaScript library for WebRTC applications
 *
 * Copyright (C) 2014-2024 Meetecho
 * License: GPLv3
 */
(function (window) {
    "use strict";

    if (typeof window === "undefined") {
        throw new Error("This script must be run in a browser environment");
    }

    var Janus = function (options) {
        if (!(this instanceof Janus)) {
            return new Janus(options);
        }
        options = options || {};
        this.server = options.server || null;
        this.sessionId = null;
        this.token = options.token || null;
        this.apiSecret = options.apiSecret || null;
        this.connected = false;
        this.transactions = {};
        this.plugins = {};
        this.ws = null;
        this.rest = null;
    };

    Janus.init = function (options) {
        options = options || {};
        if (typeof options.callback !== "function") {
            throw new Error("Janus.init needs a callback");
        }
        console.log("Janus initialization completed");
        options.callback();
    };

    Janus.isWebrtcSupported = function () {
        return !!(navigator.mediaDevices && navigator.mediaDevices.getUserMedia);
    };

    Janus.log = function (msg) {
        console.log("[Janus] " + msg);
    };

    Janus.error = function (msg) {
        console.error("[Janus] " + msg);
    };

    Janus.prototype.connect = function (callback) {
        if (!this.server) {
            callback("No Janus server provided");
            return;
        }
        var self = this;
        var createSession = {
            janus: "create",
            transaction: Janus.randomString(12)
        };

        self.transactions[createSession.transaction] = function (response) {
            if (response.janus === "success") {
                self.sessionId = response.data.id;
                self.connected = true;
                callback(null, self.sessionId);
            } else {
                callback(response.error.reason);
            }
        };

        Janus.httpPost(self.server, createSession, function (response) {
            if (response) {
                self.transactions[createSession.transaction](response);
            }
        });
    };

    Janus.httpPost = function (url, body, callback) {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    callback(JSON.parse(xhr.responseText));
                } else {
                    callback(null);
                }
            }
        };
        xhr.send(JSON.stringify(body));
    };

    Janus.randomString = function (len) {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var result = "";
        for (var i = 0; i < len; i++) {
            result += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return result;
    };

    Janus.prototype.attachPlugin = function (plugin, success, error) {
        if (!this.connected) {
            error("Not connected to Janus");
            return;
        }
        var self = this;
        var body = {
            janus: "attach",
            plugin: plugin,
            session_id: self.sessionId,
            transaction: Janus.randomString(12)
        };

        self.transactions[body.transaction] = function (response) {
            if (response.janus === "success") {
                var handle = {
                    handleId: response.data.id,
                    plugin: plugin,
                    send: function (msg) {
                        msg.session_id = self.sessionId;
                        msg.handle_id = this.handleId;
                        msg.transaction = Janus.randomString(12);
                        Janus.httpPost(self.server + "/" + self.sessionId + "/" + this.handleId, msg, function (resp) {
                            console.log("Plugin response", resp);
                        });
                    }
                };
                self.plugins[handle.handleId] = handle;
                success(handle);
            } else {
                error(response.error.reason);
            }
        };

        Janus.httpPost(self.server + "/" + self.sessionId, body, function (response) {
            if (response) {
                self.transactions[body.transaction](response);
            }
        });
    };

    window.Janus = Janus;

})(window);
