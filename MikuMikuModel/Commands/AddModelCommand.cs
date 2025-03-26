// 需要引用的命名空间
using MikuMikuLibrary.Objects;
using MikuMikuLibrary.Scripts;

public sealed class AddModelCommand : IUndoableCommand
{
    private readonly Model mNewModel;
    private readonly IList<Model> mTargetList;

    public AddModelCommand(Model model, IList<Model> targetList)
    {
        mNewModel = model.DeepClone(); // 需要实现深拷贝
        mTargetList = targetList;
    }

    public void Execute()
    {
        mTargetList.Add(mNewModel);
        Scene.Instance.OnModelListModified(); // 需要添加事件通知
    }

    public void Undo()
    {
        mTargetList.Remove(mNewModel);
        Scene.Instance.OnModelListModified();
    }
}
