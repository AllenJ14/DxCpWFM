﻿(function () {
    var a, d, e, f, g, h, i, j, k, l, m, n, o, p = [].slice,
        q = {}.hasOwnProperty,
        r = function (a, b) {
            function d() {
                this.constructor = a
            }
            for (var c in b) q.call(b, c) && (a[c] = b[c]);
            return d.prototype = b.prototype, a.prototype = new d, a.__super__ = b.prototype, a
        };
    ! function () {
        var a, b, c, d, e, f, g;
        for (g = ["ms", "moz", "webkit", "o"], c = 0, e = g.length; c < e && (f = g[c], !window.requestAnimationFrame) ; c++) window.requestAnimationFrame = window[f + "RequestAnimationFrame"], window.cancelAnimationFrame = window[f + "CancelAnimationFrame"] || window[f + "CancelRequestAnimationFrame"];
        a = null, d = 0, b = {}, requestAnimationFrame ? window.cancelAnimationFrame || (a = window.requestAnimationFrame, window.requestAnimationFrame = function (c, e) {
            var f;
            return f = ++d, a(function () {
                if (!b[f]) return c()
            }, e), f
        }, window.cancelAnimationFrame = function (a) {
            return b[a] = !0
        }) : (window.requestAnimationFrame = function (a, b) {
            var c, d, e, f;
            return c = (new Date).getTime(), f = Math.max(0, 16 - (c - e)), d = window.setTimeout(function () {
                return a(c + f)
            }, f), e = c + f, d
        }, window.cancelAnimationFrame = function (a) {
            return clearTimeout(a)
        })
    }(), o = function (a) {
        var b, c;
        for (b = Math.floor(a / 3600), c = Math.floor((a - 3600 * b) / 60), a -= 3600 * b + 60 * c, a += "", c += ""; c.length < 2;) c = "0" + c;
        for (; a.length < 2;) a = "0" + a;
        return (b = b ? b + ":" : "") + c + ":" + a
    }, m = function () {
        var a, b, c;
        return b = 1 <= arguments.length ? p.call(arguments, 0) : [], c = b[0], a = b[1], k(c.toFixed(a))
    }, n = function (a, b) {
        var c, d, e;
        d = {};
        for (c in a) q.call(a, c) && (e = a[c], d[c] = e);
        for (c in b) q.call(b, c) && (e = b[c], d[c] = e);
        return d
    }, k = function (a) {
        var b, c, d, e;
        for (a += "", c = a.split("."), d = c[0], e = "", c.length > 1 && (e = "." + c[1]), b = /(\d+)(\d{3})/; b.test(d) ;) d = d.replace(b, "$1,$2");
        return d + e
    }, l = function (a) {
        return "#" === a.charAt(0) ? a.substring(1, 7) : a
    }, j = function () {
        function a(a, b) {
            null == a && (a = !0), this.clear = null == b || b, a && AnimationUpdater.add(this)
        }
        return a.prototype.animationSpeed = 32, a.prototype.update = function (a) {
            var b;
            return null == a && (a = !1), !(!a && this.displayedValue === this.value) && (this.ctx && this.clear && this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height), b = this.value - this.displayedValue, Math.abs(b / this.animationSpeed) <= .001 ? this.displayedValue = this.value : this.displayedValue = this.displayedValue + b / this.animationSpeed, this.render(), !0)
        }, a
    }(), e = function (a) {
        function b() {
            return b.__super__.constructor.apply(this, arguments)
        }
        return r(b, a), b.prototype.displayScale = 1, b.prototype.forceUpdate = !0, b.prototype.setTextField = function (a, b) {
            return this.textField = a instanceof i ? a : new i(a, b)
        }, b.prototype.setMinValue = function (a, b) {
            var c, d, e, f, g;
            if (this.minValue = a, null == b && (b = !0), b) {
                for (this.displayedValue = this.minValue, f = this.gp || [], g = [], d = 0, e = f.length; d < e; d++) c = f[d], g.push(c.displayedValue = this.minValue);
                return g
            }
        }, b.prototype.setOptions = function (a) {
            return null == a && (a = null), this.options = n(this.options, a), this.textField && (this.textField.el.style.fontSize = a.fontSize + "px"), this.options.angle > .5 && (this.options.angle = .5), this.configDisplayScale(), this
        }, b.prototype.configDisplayScale = function () {
            var a, b, c, d, e;
            return d = this.displayScale, !1 === this.options.highDpiSupport ? delete this.displayScale : (b = window.devicePixelRatio || 1, a = this.ctx.webkitBackingStorePixelRatio || this.ctx.mozBackingStorePixelRatio || this.ctx.msBackingStorePixelRatio || this.ctx.oBackingStorePixelRatio || this.ctx.backingStorePixelRatio || 1, this.displayScale = b / a), this.displayScale !== d && (e = this.canvas.G__width || this.canvas.width, c = this.canvas.G__height || this.canvas.height, this.canvas.width = e * this.displayScale, this.canvas.height = c * this.displayScale, this.canvas.style.width = e + "px", this.canvas.style.height = c + "px", this.canvas.G__width = e, this.canvas.G__height = c), this
        }, b.prototype.parseValue = function (a) {
            return a = parseFloat(a) || Number(a), isFinite(a) ? a : 0
        }, b
    }(j), i = function () {
        function a(a, b) {
            this.el = a, this.fractionDigits = b
        }
        return a.prototype.render = function (a) {
            return this.el.innerHTML = m(a.displayedValue, this.fractionDigits)
        }, a
    }(), a = function (a) {
        function b(a, b) {
            this.elem = a, this.text = null != b && b, this.value = 1 * this.elem.innerHTML, this.text && (this.value = 0)
        }
        return r(b, a), b.prototype.displayedValue = 0, b.prototype.value = 0, b.prototype.setVal = function (a) {
            return this.value = 1 * a
        }, b.prototype.render = function () {
            var a;
            return a = this.text ? o(this.displayedValue.toFixed(0)) : k(m(this.displayedValue)), this.elem.innerHTML = a
        }, b
    }(j), h = function (a) {
        function b(a) {
            this.gauge = a, this.ctx = this.gauge.ctx, this.canvas = this.gauge.canvas, b.__super__.constructor.call(this, !1, !1), this.setOptions()
        }
        return r(b, a), b.prototype.displayedValue = 0, b.prototype.value = 0, b.prototype.options = {
            strokeWidth: .035,
            length: .1,
            color: "#000000",
            iconPath: null,
            iconScale: 1,
            iconAngle: 0
        }, b.prototype.img = null, b.prototype.setOptions = function (a) {
            if (null == a && (a = null), this.options = n(this.options, a), this.length = 2 * this.gauge.radius * this.gauge.options.radiusScale * this.options.length, this.strokeWidth = this.canvas.height * this.options.strokeWidth, this.maxValue = this.gauge.maxValue, this.minValue = this.gauge.minValue, this.animationSpeed = this.gauge.animationSpeed, this.options.angle = this.gauge.options.angle, this.options.iconPath) return this.img = new Image, this.img.src = this.options.iconPath
        }, b.prototype.render = function () {
            var a, b, c, d, e, f, g, h, i;
            if (a = this.gauge.getAngle.call(this, this.displayedValue), h = Math.round(this.length * Math.cos(a)), i = Math.round(this.length * Math.sin(a)), f = Math.round(this.strokeWidth * Math.cos(a - Math.PI / 2)), g = Math.round(this.strokeWidth * Math.sin(a - Math.PI / 2)), b = Math.round(this.strokeWidth * Math.cos(a + Math.PI / 2)), c = Math.round(this.strokeWidth * Math.sin(a + Math.PI / 2)), this.ctx.fillStyle = this.options.color, this.ctx.beginPath(), this.ctx.arc(0, 0, this.strokeWidth, 0, 2 * Math.PI, !0), this.ctx.fill(), this.ctx.beginPath(), this.ctx.moveTo(f, g), this.ctx.lineTo(h, i), this.ctx.lineTo(b, c), this.ctx.fill(), this.img) return d = Math.round(this.img.width * this.options.iconScale), e = Math.round(this.img.height * this.options.iconScale), this.ctx.save(), this.ctx.translate(h, i), this.ctx.rotate(a + Math.PI / 180 * (90 + this.options.iconAngle)), this.ctx.drawImage(this.img, -d / 2, -e / 2, d, e), this.ctx.restore()
        }, b
    }(j),
        function () {
            function a(a) {
                this.elem = a
            }
            a.prototype.updateValues = function (a) {
                return this.value = a[0], this.maxValue = a[1], this.avgValue = a[2], this.render()
            }, a.prototype.render = function () {
                var a, b;
                return this.textField && this.textField.text(m(this.value)), 0 === this.maxValue && (this.maxValue = 2 * this.avgValue), b = this.value / this.maxValue * 100, a = this.avgValue / this.maxValue * 100, $(".bar-value", this.elem).css({
                    width: b + "%"
                }), $(".typical-value", this.elem).css({
                    width: a + "%"
                })
            }
        }(), g = function (a) {
            function b(a) {
                var c, d;
                this.canvas = a, b.__super__.constructor.call(this), this.percentColors = null, "undefined" != typeof G_vmlCanvasManager && (this.canvas = window.G_vmlCanvasManager.initElement(this.canvas)), this.ctx = this.canvas.getContext("2d"), c = this.canvas.clientHeight, d = this.canvas.clientWidth, this.canvas.height = c, this.canvas.width = d, this.gp = [new h(this)], this.setOptions(), this.render()
            }
            return r(b, a), b.prototype.elem = null, b.prototype.value = [20], b.prototype.maxValue = 80, b.prototype.minValue = 0, b.prototype.displayedAngle = 0, b.prototype.displayedValue = 0, b.prototype.lineWidth = 40, b.prototype.paddingTop = .0, b.prototype.paddingBottom = .2, b.prototype.percentColors = null, b.prototype.options = {
                colorStart: "#6fadcf",
                colorStop: void 0,
                gradientType: 0,
                strokeColor: "#e0e0e0",
                pointer: {
                    length: .8,
                    strokeWidth: .035,
                    iconScale: 1
                },
                angle: .15,
                lineWidth: .44,
                radiusScale: 1,
                fontSize: 40,
                limitMax: !1,
                limitMin: !1
            }, b.prototype.setOptions = function (a) {
                var c, d, e, f, g;
                for (null == a && (a = null), b.__super__.setOptions.call(this, a), this.configPercentColors(), this.extraPadding = 0, this.options.angle < 0 && (f = Math.PI * (1 + this.options.angle), this.extraPadding = Math.sin(f)), this.availableHeight = this.canvas.height * (1 - this.paddingTop - this.paddingBottom), this.lineWidth = this.availableHeight * this.options.lineWidth, this.radius = (this.availableHeight - this.lineWidth / 2) / (1 + this.extraPadding), this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height), g = this.gp, d = 0, e = g.length; d < e; d++) c = g[d], c.setOptions(this.options.pointer), c.render();
                return this
            }, b.prototype.configPercentColors = function () {
                var a, b, c, d, e, f, g;
                if (this.percentColors = null, void 0 !== this.options.percentColors) {
                    for (this.percentColors = new Array, f = [], c = d = 0, e = this.options.percentColors.length - 1; 0 <= e ? d <= e : d >= e; c = 0 <= e ? ++d : --d) g = parseInt(l(this.options.percentColors[c][1]).substring(0, 2), 16), b = parseInt(l(this.options.percentColors[c][1]).substring(2, 4), 16), a = parseInt(l(this.options.percentColors[c][1]).substring(4, 6), 16), f.push(this.percentColors[c] = {
                        pct: this.options.percentColors[c][0],
                        color: {
                            r: g,
                            g: b,
                            b: a
                        }
                    });
                    return f
                }
            }, b.prototype.set = function (a) {
                var b, c, d, e, f, g, i, j, k;
                for (a instanceof Array || (a = [a]), c = d = 0, i = a.length - 1; 0 <= i ? d <= i : d >= i; c = 0 <= i ? ++d : --d) a[c] = this.parseValue(a[c]);
                if (a.length > this.gp.length)
                    for (c = e = 0, j = a.length - this.gp.length; 0 <= j ? e < j : e > j; c = 0 <= j ? ++e : --e) b = new h(this), b.setOptions(this.options.pointer), this.gp.push(b);
                else a.length < this.gp.length && (this.gp = this.gp.slice(this.gp.length - a.length));
                for (c = 0, f = 0, g = a.length; f < g; f++) k = a[f], k > this.maxValue ? this.options.limitMax ? k = this.maxValue : this.maxValue = k + 1 : k < this.minValue && (this.options.limitMin ? k = this.minValue : this.minValue = k - 1), this.gp[c].value = k, this.gp[c++].setOptions({
                    minValue: this.minValue,
                    maxValue: this.maxValue,
                    angle: this.options.angle
                });
                return this.value = Math.max(Math.min(a[a.length - 1], this.maxValue), this.minValue), AnimationUpdater.run(this.forceUpdate), this.forceUpdate = !1
            }, b.prototype.getAngle = function (a) {
                return (1 + this.options.angle) * Math.PI + (a - this.minValue) / (this.maxValue - this.minValue) * (1 - 2 * this.options.angle) * Math.PI
            }, b.prototype.getColorForPercentage = function (a, b) {
                var c, d, e, f, g, h, i;
                if (0 === a) c = this.percentColors[0].color;
                else
                    for (c = this.percentColors[this.percentColors.length - 1].color, e = f = 0, h = this.percentColors.length - 1; 0 <= h ? f <= h : f >= h; e = 0 <= h ? ++f : --f)
                        if (a <= this.percentColors[e].pct) {
                            !0 === b ? (i = this.percentColors[e - 1] || this.percentColors[0], d = this.percentColors[e], g = (a - i.pct) / (d.pct - i.pct), c = {
                                r: Math.floor(i.color.r * (1 - g) + d.color.r * g),
                                g: Math.floor(i.color.g * (1 - g) + d.color.g * g),
                                b: Math.floor(i.color.b * (1 - g) + d.color.b * g)
                            }) : c = this.percentColors[e].color;
                            break
                        } return "rgb(" + [c.r, c.g, c.b].join(",") + ")"
            }, b.prototype.getColorForValue = function (a, b) {
                var c;
                return c = (a - this.minValue) / (this.maxValue - this.minValue), this.getColorForPercentage(c, b)
            }, b.prototype.renderStaticLabels = function (a, b, c, d) {
                var e, f, g, h, i, j, k, l, n, o;
                for (this.ctx.save(), this.ctx.translate(b, c), e = a.font || "10px Times", j = /\d+\.?\d?/, i = e.match(j)[0], l = e.slice(i.length), f = parseFloat(i) * this.displayScale, this.ctx.font = "bold " + f + l, this.ctx.fillStyle = a.color || "#000000", this.ctx.textBaseline = "bottom", this.ctx.textAlign = "center", k = a.labels, g = 0, h = k.length; g < h; g++)
                { o = k[g], (!this.options.limitMin || o >= this.minValue) && (!this.options.limitMax || o <= this.maxValue) && (n = this.getAngle(o) - 3 * Math.PI / 2, this.ctx.rotate(n), this.ctx.fillText(m(o, a.fractionDigits), 0, -d - this.lineWidth / 2)); switch (o) { case 18: this.ctx.fillText("Min", 0, -17 - d - this.lineWidth / 2); break; case 24: this.ctx.fillText("Ideal", 0, -17 - d - this.lineWidth / 2); break; case 30: this.ctx.fillText("Max", 0, -17 - d - this.lineWidth / 2); break; }; this.ctx.rotate(-n); }
                return this.ctx.restore()
            }, b.prototype.render = function () {
                var a, b, c, d, e, f, g, h, i, j, k, l, m, n, o;
                if (n = this.canvas.width / 2, d = this.canvas.height * this.paddingTop + this.availableHeight - (this.radius + this.lineWidth / 2) * this.extraPadding, a = this.getAngle(this.displayedValue), this.textField && this.textField.render(this), this.ctx.lineCap = "butt", k = this.radius * this.options.radiusScale, this.options.staticLabels && this.renderStaticLabels(this.options.staticLabels, n, d, k), this.options.staticZones) {
                    for (this.ctx.save(), this.ctx.translate(n, d), this.ctx.lineWidth = this.lineWidth, l = this.options.staticZones, e = 0, g = l.length; e < g; e++) o = l[e], j = o.min, this.options.limitMin && j < this.minValue && (j = this.minValue), i = o.max, this.options.limitMax && i > this.maxValue && (i = this.maxValue), this.ctx.strokeStyle = o.strokeStyle, this.ctx.beginPath(), this.ctx.arc(0, 0, k, this.getAngle(j), this.getAngle(i), !1), this.ctx.stroke();
                    this.ctx.restore()
                } else void 0 !== this.options.customFillStyle ? b = this.options.customFillStyle(this) : null !== this.percentColors ? b = this.getColorForValue(this.displayedValue, !0) : void 0 !== this.options.colorStop ? (b = 0 === this.options.gradientType ? this.ctx.createRadialGradient(n, d, 9, n, d, 70) : this.ctx.createLinearGradient(0, 0, n, 0), b.addColorStop(0, this.options.colorStart), b.addColorStop(1, this.options.colorStop)) : b = this.options.colorStart, this.ctx.strokeStyle = b, this.ctx.beginPath(), this.ctx.arc(n, d, k, (1 + this.options.angle) * Math.PI, a, !1), this.ctx.lineWidth = this.lineWidth, this.ctx.stroke(), this.ctx.strokeStyle = this.options.strokeColor, this.ctx.beginPath(), this.ctx.arc(n, d, k, a, (2 - this.options.angle) * Math.PI, !1), this.ctx.stroke();
                for (this.ctx.translate(n, d), m = this.gp, f = 0, h = m.length; f < h; f++) c = m[f], c.update(!0);
                return this.ctx.translate(-n, -d)
            }, b
        }(e), d = function (a) {
            function b(a) {
                this.canvas = a, b.__super__.constructor.call(this), "undefined" != typeof G_vmlCanvasManager && (this.canvas = window.G_vmlCanvasManager.initElement(this.canvas)), this.ctx = this.canvas.getContext("2d"), this.setOptions(), this.render()
            }
            return r(b, a), b.prototype.lineWidth = 15, b.prototype.displayedValue = 0, b.prototype.value = 33, b.prototype.maxValue = 80, b.prototype.minValue = 0, b.prototype.options = {
                lineWidth: .1,
                colorStart: "#6f6ea0",
                colorStop: "#c0c0db",
                strokeColor: "#eeeeee",
                shadowColor: "#d5d5d5",
                angle: .35,
                radiusScale: 1
            }, b.prototype.getAngle = function (a) {
                return (1 - this.options.angle) * Math.PI + (a - this.minValue) / (this.maxValue - this.minValue) * (2 + this.options.angle - (1 - this.options.angle)) * Math.PI
            }, b.prototype.setOptions = function (a) {
                return null == a && (a = null), b.__super__.setOptions.call(this, a), this.lineWidth = this.canvas.height * this.options.lineWidth, this.radius = this.options.radiusScale * (this.canvas.height / 2 - this.lineWidth / 2), this
            }, b.prototype.set = function (a) {
                return this.value = this.parseValue(a), this.value > this.maxValue && (this.maxValue = 1.1 * this.value), AnimationUpdater.run(this.forceUpdate), this.forceUpdate = !1
            }, b.prototype.render = function () {
                var a, b, c, f;
                return a = this.getAngle(this.displayedValue), f = this.canvas.width / 2, c = this.canvas.height / 2, this.textField && this.textField.render(this), b = this.ctx.createRadialGradient(f, c, 39, f, c, 70), b.addColorStop(0, this.options.colorStart), b.addColorStop(1, this.options.colorStop), this.radius - this.lineWidth / 2, this.radius + this.lineWidth / 2, this.ctx.strokeStyle = this.options.strokeColor, this.ctx.beginPath(), this.ctx.arc(f, c, this.radius, (1 - this.options.angle) * Math.PI, (2 + this.options.angle) * Math.PI, !1), this.ctx.lineWidth = this.lineWidth, this.ctx.lineCap = "round", this.ctx.stroke(), this.ctx.strokeStyle = b, this.ctx.beginPath(), this.ctx.arc(f, c, this.radius, (1 - this.options.angle) * Math.PI, a, !1), this.ctx.stroke()
            }, b
        }(e), f = function (a) {
            function b() {
                return b.__super__.constructor.apply(this, arguments)
            }
            return r(b, a), b.prototype.strokeGradient = function (a, b, c, d) {
                var e;
                return e = this.ctx.createRadialGradient(a, b, c, a, b, d), e.addColorStop(0, this.options.shadowColor), e.addColorStop(.12, this.options._orgStrokeColor), e.addColorStop(.88, this.options._orgStrokeColor), e.addColorStop(1, this.options.shadowColor), e
            }, b.prototype.setOptions = function (a) {
                var c, d, e, f;
                return null == a && (a = null), b.__super__.setOptions.call(this, a), f = this.canvas.width / 2, c = this.canvas.height / 2, d = this.radius - this.lineWidth / 2, e = this.radius + this.lineWidth / 2, this.options._orgStrokeColor = this.options.strokeColor, this.options.strokeColor = this.strokeGradient(f, c, d, e), this
            }, b
        }(d), window.AnimationUpdater = {
            elements: [],
            animId: null,
            addAll: function (a) {
                var b, c, d, e;
                for (e = [], c = 0, d = a.length; c < d; c++) b = a[c], e.push(AnimationUpdater.elements.push(b));
                return e
            },
            add: function (a) {
                return AnimationUpdater.elements.push(a)
            },
            run: function (a) {
                var b, c, d, e, f;
                for (null == a && (a = !1), b = !0, f = AnimationUpdater.elements, d = 0, e = f.length; d < e; d++) c = f[d], c.update(!0 === a) && (b = !1);
                return b ? cancelAnimationFrame(AnimationUpdater.animId) : AnimationUpdater.animId = requestAnimationFrame(AnimationUpdater.run)
            }
        }, "function" == typeof window.define && null != window.define.amd ? define(function () {
            return {
                Gauge: g,
                Donut: f,
                BaseDonut: d,
                TextRenderer: i
            }
        }) : "undefined" != typeof module && null != module.exports ? module.exports = {
            Gauge: g,
            Donut: f,
            BaseDonut: d,
            TextRenderer: i
        } : (window.Gauge = g, window.Donut = f, window.BaseDonut = d, window.TextRenderer = i)
}).call(this);