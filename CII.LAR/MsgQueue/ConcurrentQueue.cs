using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.MsgQueue
{
    /// <summary>
    /// 安全队列
    /// </summary>
    /// <typeparam name="T">队列中元素类型</typeparam>
    /// <remark>
    /// 思路：用lock封装Queue_T
    /// 公司：CASCO
    /// 作者：戴唯艺
    /// 创建日期：2013-05-13
    /// 说明：未使用.Net自带的ConcurrentQueue是为了增加灵活性，特别是针对Push、Pop一个集合的时候
    /// </remark>
    public class ConcurrentQueue<T>
    {
        readonly Queue<T> queue = new Queue<T>();
        public int Count { get { return queue.Count; } }
        /// <summary>
        /// 添加数据到末端
        /// </summary>
        public void Push(T obj)
        {
            lock (this)
            {
                queue.Enqueue(obj);
            }
        }
        /// <summary>
        /// 添加集合
        /// </summary>
        public void Push(IEnumerable<T> list)
        {
            lock (this)
            {
                foreach (var item in list)
                {
                    queue.Enqueue(item);
                }
            }
        }
        /// <summary>
        /// 取首端数据，并删除
        /// </summary>
        /// <returns>首端数据</returns>
        public T Pop()
        {
            lock (this)
            {
                var obj = queue.Count != 0 ? queue.Dequeue() : default(T);
                return obj;
            }
        }
        /// <summary>
        /// 所有数据出队列
        /// </summary>
        /// <returns></returns>
        public List<T> PopAll()
        {
            lock (this)
            {
                var list = new List<T>();
                while (queue.Count != 0)
                {
                    list.Add(queue.Dequeue());
                }
                return list;
            }
        }
    }
}
