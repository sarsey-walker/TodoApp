using Microsoft.JSInterop;

namespace TodoApp
{
    public static class MyJsInterop
    {

        public static bool IsReady = false;
        public static IJSRuntime jsRuntime = null;

        //public static ValueTask<bool> Alert(string msg)
        //{
        //    return jsRuntime.InvokeAsync<bool>(
        //        "MyJSFunctions.Alert", msg);
        //}


        //public static ValueTask<string> getCaretCoordinates(string el)
        //{
        //    return jsRuntime.InvokeAsync<string>(
        //        "MyJSFunctions.getCaretCoordinates", el);
        //}




        //public static ValueTask<bool> Log(string msg)
        //{

        //    return jsRuntime.InvokeAsync<bool>(
        //        "MyJSFunctions.Log", msg);
        //}


        //public static ValueTask<double> GetElementActualTop(string elementID)
        //{
        //    return jsRuntime.InvokeAsync<double>(
        //        "MyJSFunctions.GetElementActualTop", elementID);
        //}

        //public static ValueTask<double> GetElementActualLeft(string elementID)
        //{
        //    return jsRuntime.InvokeAsync<double>(
        //        "MyJSFunctions.GetElementActualLeft", elementID);
        //}

        public static ValueTask<bool> setTextToCurrentPos(string elementID, string value)
        {
             return jsRuntime.InvokeAsync<bool>(
                "MyJSFunctions.setTextToCurrentPos", elementID, value);
        }


    }
}
