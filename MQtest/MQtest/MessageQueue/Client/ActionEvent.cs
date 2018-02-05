﻿namespace TJAP.Common.Tools.MessageQueue.Client
{
    /// <summary>
    /// 表示消息到达客户端发起的事件。
    /// </summary>
    /// <param name="result">EventMessageResult 事件消息对象</param>
    public delegate void ActionEvent(EventMessageResult result);
}