public class AddModelCommand : INodeCommand
{
    private ModelNode mNewModelNode;
    private INode mParentNode;

    public AddModelCommand(ModelNode baseNode, INode parentNode)
    {
        // 需要实现深拷贝
        mNewModelNode = DeepCloneModelNode(baseNode); 
        mParentNode = parentNode;
    }

    public override void Execute(INodeCommandContext context)
    {
        context.AddNode(mNewModelNode, mParentNode);
        context.SelectNode(mNewModelNode);
    }

    public override void Undo(INodeCommandContext context)
    {
        context.RemoveNode(mNewModelNode);
    }
}
