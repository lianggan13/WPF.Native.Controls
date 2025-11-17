namespace WPF.Thumb.ThumbFlow
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls.Primitives;
    using System.Windows.Shapes;

    public class FlowNodeBase : Thumb
    {
        public FlowNodeBase PreviousNode { get; set; }
        public FlowNodeBase NextNode { get; set; }
        public Path InputPath { get; set; }
        public Path OutputPath { get; set; }

        public Dictionary<string, string> Values = new Dictionary<string, string>();

        public virtual void Process()
        {
            if (NextNode != null)
            {
                NextNode.Process();
            }
        }
    }

    public class FlowNodeA : FlowNodeBase
    {
        public FlowNodeA()
        {
            Values.Add("_imagePtr", "iHandle");
        }

        public override void Process()
        {
            Console.WriteLine($"FlowNodeA 执行了 ---");

            base.Process();
        }
    }

    public class FlowNodeB : FlowNodeBase
    {
        public FlowNodeB()
        {
            Values.Add("imageHandle", "iHandle");
        }
        public override void Process()
        {
            Console.WriteLine($"FlowNodeB 执行了---");

            base.Process();
        }
    }

    public class FlowNodeC : FlowNodeBase
    {
        //public List<FlowNodeBase> SubNodes { get; set; }
        public override void Process()
        {
            Console.WriteLine("FlowNodeC 执行了");

            base.Process();
        }
        //  WhileNode -> SubNodeA -> SubNodeB  -> SubNodeC -> WhileNode
        //  条件判断 
        //  NextNode
    }

    public class FlowNodeD : FlowNodeBase
    {
        //public override void Process()
        //{
        //    base.Process();
        //}
    }

}
