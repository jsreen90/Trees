using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trees
{
    class Program
    {
        public static Node NodeData;
        static void Main(string[] args)
        {
            try
            {


                Console.WriteLine("Welcome to tree Creator!" + System.Environment.NewLine + "Key in 1 to Continue or 0 to exit");
                int reply;
                reply = Int32.Parse(Console.ReadLine());
                if (reply == 1)
                {
                    NodeData = new Node();
                    bool looper = true;
                    while (looper)
                    {
                        Console.WriteLine("You can perform the Following Activity: " + System.Environment.NewLine + "1-add Node, 2-Remove node,3- Visualize Tree, 4-Exit");
                        reply = Int32.Parse(Console.ReadLine());
                        if (reply != 4)
                        {
                            TakeRequest(reply);
                        }
                        else
                        {
                            Console.WriteLine("! Thank you for using Tree Builder !");
                            System.Threading.Thread.Sleep(500);
                            System.Environment.Exit(1);
                        }
                    }
                }
                else if (reply == 0)
                {
                    Console.WriteLine("Thank you for using Tree Builder:");
                    System.Threading.Thread.Sleep(500);
                    System.Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("Invalid Reply");
                    Main(new string[] { });
                }
                int a = Int32.Parse(Console.ReadLine());
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Operation Performed...restarting activities");
                Main(new string[] { });
                // System.Environment.Exit(1);
            }
        }
        public static void TakeRequest(int requesthandle)
        {
            try
            {
                switch (requesthandle)
                {
                    case 1:
                        if (NodeData.GetChildren().Count == 0)
                        {
                            Console.WriteLine("Enter the Data for Root Node:");
                            int data = Int32.Parse(Console.ReadLine());
                            NodeData.data = data;
                            NodeData.Level = 0;
                            Console.WriteLine("Root Node Created, Key in the number of children and the data for each, all separeated by space --> <numchild> <val1> <val2> <val3>...:");
                            int[] invals = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                            for (int i = 0; i <= invals[0] - 1; i++)
                            {
                                Node child = new Node(invals[i + 1], 1, NodeData);
                                NodeData.AddChildNode(child);
                            }
                            Console.WriteLine("Creation Successful");


                        }
                        else
                        {
                            Console.WriteLine("Key in the Values--> <Level> <Parent_Node_Data> <numchild> <val1> <val2> <val3>...:");
                            int[] invals = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                            //get the node at that level with the data mentioned for that node
                            //We can get that recursively...

                            Node InsertNode = null;
                            if(invals[0]==0)
                            {
                                InsertNode = NodeData.GetNode();
                            }
                            else
                            {
                                GetNode(ref NodeData, invals[0], invals[1], ref InsertNode);
                            }
                            
                            if (InsertNode != null)
                            {
                                for (int i = 0; i <= invals[2] - 1; i++)
                                {
                                    Node child = new Node(invals[i + 3], invals[0] + 1, InsertNode);

                                    InsertNode.AddChildNode(child);
                                }
                                Console.WriteLine("Creation Successful");
                            }
                        }


                        break;

                    case 2:

                        Console.WriteLine("Key in the values--> <Level> <Node_Data>:");
                        int[] inpvals = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                        //get the node at that level with the data mentioned for that node
                        //We can get that recursively...

                        Node InNode = null;
                        GetNode(ref NodeData, inpvals[0], inpvals[1], ref InNode);
                        InNode.GetParent().RemoveChildNode(InNode);
                        Console.WriteLine("Removal Successful");
                        break;

                    case 3:
                        if (NodeData.GetChildren().Count > 0)
                        {
                            GenerateTree();
                        }
                        else { Console.WriteLine("Create Tree in order to view it"); }

                        break;
                }

            }
            catch (Exception ex) { throw ex; }
        }

        private static Node GetNode(ref Node thenode, int Level, int PND, ref Node Got)
        {
            try
            {
                Node GotNode;
                Node ChosenNode;

                foreach (Node item in thenode.GetChildren())
                {
                    ChosenNode = item;
                    if (item.GetLevel() == Level)
                    {
                        if (item.GetData() == PND)
                        {
                            GotNode = item.GetNode();
                            Got = GotNode;

                        }
                        else { continue; }
                    }
                    else
                    {
                        if (ChosenNode.GetChildren().Count > 0)
                        {
                            GetNode(ref ChosenNode, Level, PND, ref Got);
                        }
                        else { continue; }

                    }
                }
                return null;
            }



            catch (Exception ex) { return null; }
        }
        public static void GenerateTree()
        {
            Form f1 = new Form();
            f1.Text = "Tree Generator";
            f1.ShowIcon = false;
            TreeView tv = new TreeView();
            TreeNode tn = new TreeNode();
            tn.Text = NodeData.data.ToString();
            tv.Nodes.Add(tn);
            //MAke the tree now and add to the View
            displaytheData(ref tv, ref tn, ref NodeData);

            tv.Dock = DockStyle.Fill;
            f1.Controls.Add(tv);
            f1.ShowDialog();
        }

        private static void displaytheData(ref TreeView tv, ref TreeNode tn, ref Node TheNode)
        {
            try
            {
                TreeNode tnn;
                Node selected;
                foreach (Node item in TheNode.GetChildren())
                {
                    selected = item;
                    tnn = new TreeNode();
                    tnn.Text = item.GetData().ToString();
                    tn.Nodes.Add(tnn);
                    if (item.GetChildren().Count > 0)
                    {
                        displaytheData(ref tv, ref  tnn, ref selected);
                    }
                }

            }
            catch (Exception ex)
            { }
        }


    }
}
