﻿/****************************************************************************
*项目名称：SAEA.Http2
*CLR 版本：4.0.30319.42000
*机器名称：WENLI-PC
*命名空间：SAEA.Http2
*类 名 称：WebInvoker
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：wenguoli_520@qq.com
*创建时间：2019/6/28 15:54:20
*描述：
*=====================================================================
*修改时间：2019/6/28 15:54:20
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using SAEA.Common;
using SAEA.Http2.Extentions;
using SAEA.Http2.Model;
using SAEA.Http2.Net;

namespace SAEA.Http2
{
    public class WebInvoker : IHttp2Invoker
    {
        public WebInvoker(string root = "")
        {
            PathExtentions.SetRoot(root);
        }

        public void Invoke(Http2Context http2Context)
        {
            var path = http2Context.Request.Path;

            if (path == "/")
            {
                path = "/index.html";
            }

            switch (http2Context.Request.Method)
            {
                case "GET":

                    var filePath = PathExtentions.MapPath(path);

                    if (FileHelper.Exists(filePath))
                    {
                        var responseHeaders1 = new HeaderField[]
                        {
                            new HeaderField { Name = ":status", Value = "200" },
                            new HeaderField { Name = "date", Value = DateTime.Now.ToGMTString() },
                            new HeaderField { Name = "server", Value = "SAEA.Http2Server" },
                            new HeaderField { Name = "content-type", Value = "text/html" },
                        };

                        http2Context.Response.SetHeaders(responseHeaders1);

                        http2Context.Response.Write(FileHelper.Read(filePath));
                    }
                    else
                    {
                        var responseHeaders2 = new HeaderField[]
                        {
                            new HeaderField { Name = ":status", Value = "404" },
                            new HeaderField { Name = "date", Value = DateTime.Now.ToGMTString() },
                            new HeaderField { Name = "server", Value = "SAEA.Http2Server" },
                            new HeaderField { Name = "content-type", Value = "text/html" },
                        };

                        http2Context.Response.SetHeaders(responseHeaders2);

                        http2Context.Response.Write("找不到内容！");
                    }

                    break;
                case "POST":

                    var responseHeaders3 = new HeaderField[]
                       {
                            new HeaderField { Name = ":status", Value = "204" },
                            new HeaderField { Name = "date", Value = DateTime.Now.ToGMTString() },
                            new HeaderField { Name = "server", Value = "SAEA.Http2Server" },
                            new HeaderField { Name = "content-type", Value = "text/html" },
                       };

                    http2Context.Response.SetHeaders(responseHeaders3);

                    http2Context.Response.Write(http2Context.Request.Body);


                    break;
                default:

                    break;
            }

            http2Context.Response.End();


        }
    }
}