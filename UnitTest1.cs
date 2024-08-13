namespace XunitFixtureSink;

public class UnitTest1 : IClassFixture<SampleFixture>
{
    public UnitTest1(SampleFixture fixture)
    {
        _ = fixture;
    }

    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }
}

public class SampleFixture
{
    public SampleFixture(IMessageSink messageSink)
    {
        messageSink.OnMessage(new DiagnosticMessage("Hello from message sink"));
    }
}
