3/02/25:  
Currently, I piggyback off of WinForms and use GDI+ calls for everything. This obviously provokes an issue: WinForms sucks. Initially, this was chosen simply because I wanted a graphics context to be able to draw to.   
As it stands, rendering code and the main phengine code are in the same file. This is not ideal, especially as other components of phengine are separate (eg. GameObject class, Components, et cetera.).  

My goal eventually is to make my way over to using Veldrid for rendering, however this is far more low-level than I myself know. I would like to have phengine be cross-platform to some degree.  

From the suggestion of a friend, if I find Veldrid terrifying at first - I should consider piggybacking off of something like FNA/MonoGame exclusively for its rendering functionality. This idea makes sense for me currently.  

I will look into Veldrid and how it functions more over the coming weeks, however I will continue to work on phengine as it is  and get the baseline engine to a certain level of functionality before I begin considering swapping graphics libraries around.