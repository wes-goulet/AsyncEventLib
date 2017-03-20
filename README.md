# AsyncEventLib
*A simple library for awaiting asynchronous event handlers*


[![Build Status](https://travis-ci.org/wes566/AsyncEventLib.svg?branch=master)](https://travis-ci.org/wes566/AsyncEventLib)

## Example
``` cs
public event EventHandler<AsyncEventArgs> TestEvent;

public async Task RaiseEventAsync()
{
    await TestEvent.InvokeAsync();
    Log("Done invoking event handlers");
}

public void RegisterAwaitedAsyncHandler()
{
    TestEvent += (sender, args) =>
        {
            args.RegisterFuncToBeAwaitedByEventPublisher(async () =>
            {
                await Task.Delay(50);
                Log("async handler done");
            });
        };
}

/* LOG OUTPUT:
async handler done
Done invoking event handlers
*/
```

[Contributions welcome!](CONTRIBUTE.md)

[MIT License](LICENSE)