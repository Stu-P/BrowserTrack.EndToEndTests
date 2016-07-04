namespace SpecResults.Model
{
	public class ScenarioBlock : Step
	{
        public BlockTypeEnum BlockType;

		public override TestResult Result
		{
			get { return Steps.GetResult(); }
		}
	}

    public enum BlockTypeEnum { Given, When, Then }
}