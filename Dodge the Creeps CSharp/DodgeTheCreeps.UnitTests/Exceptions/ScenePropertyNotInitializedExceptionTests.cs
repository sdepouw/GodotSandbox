using DodgeTheCreeps.Core.Exceptions;
using Shouldly;

namespace DodgeTheCreeps.UnitTests.Exceptions;

public class ScenePropertyNotInitializedExceptionTests
{
  [Test]
  public void IsACriticalException()
  {
    Type expectedBaseType = typeof(GodotCriticalException);
    
    Type exceptionType = typeof(ScenePropertyNotInitializedException<>);
    
    exceptionType.BaseType.ShouldBe(expectedBaseType);
  }
}
