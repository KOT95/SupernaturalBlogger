using UnityEngine;

public class Block
{
    public int Supernatural;
    public int SupernaturalMiddle;
    public int SupernaturalHigh;
}

public class LevelSupernatural : MonoBehaviour
{
    [SerializeField] private RandomSupernatural randomSupernatural = new RandomSupernatural();
    
    public Block Block { get; private set; }

    public void AddCount(int count, TypeSupernatural type)
    {
        Block.Supernatural += type == TypeSupernatural.Low ? count : 0;
        Block.SupernaturalMiddle += type == TypeSupernatural.Middle ? count : 0;
        Block.SupernaturalHigh += type == TypeSupernatural.High ? count : 0;

        int totalNumber = Block.Supernatural + Block.SupernaturalMiddle;
        UIController._instance.UpdateTextSupernaturalCounter(totalNumber);
    }

    public void RandomActivate() { randomSupernatural.Activate(); }
    public void RandomDeactivate() { randomSupernatural.Deactivate(); }

    public void ResetSupernatural()
    {
        Block = new Block();
        randomSupernatural.Reset();
    }
}
