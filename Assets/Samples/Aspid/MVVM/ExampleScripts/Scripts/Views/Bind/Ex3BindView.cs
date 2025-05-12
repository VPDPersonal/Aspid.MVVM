using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.ExampleScripts.Views.Bind
{
    [View]
    public partial class Ex3BindView
    {
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image _image;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image[] _images;
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertyImage { get; }
        
        [AsBinder(typeof(ImageSpriteBinder))]
        private Image PropertyImages { get; }
    }
}

//  Generated Code:
//  public partial class Ex3BindView : IView
//  {
//      private ImageSpriteBinder __imageCachedBinder;
//      private ImageSpriteBinder[] __imagesCachedBinder;
//      private ImageSpriteBinder __propertyImageCachedPropertyBinder;
//      private ImageSpriteBinder __propertyImagesCachedPropertyBinder;
//      
//  	public IViewModel ViewModel { get; private set; }
//  	
//  	public void Initialize(IViewModel viewModel)
//  	{
//  	    if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
//  	    if (ViewModel is not null) throw new InvalidOperationException("View is already initialized.");
//  	    
//  	    ViewModel = viewModel;
//  	    InitializeInternal(viewModel);
//  	}
//  	
//  	protected virtual void InitializeInternal(IViewModel viewModel)
//      {
//          OnInitializingInternal(viewModel);
//          InstantiateBinders();
//              
//          __imageCachedBinder.BindSafely(viewModel.FindBindableMember(new(Generated.Ids.Image)));
//          __imagesCachedBinder.BindSafely(viewModel.FindBindableMember(new(Generated.Ids.Images)));
//          __propertyImageCachedPropertyBinder.BindSafely(viewModel.FindBindableMember(new(Generated.Ids.PropertyImage)));
//          __propertyImagesCachedPropertyBinder.BindSafely(viewModel.FindBindableMember(new(Generated.Ids.PropertyImages)));
//              
//          OnInitializedInternal(viewModel);
//      }
//  	
//  	partial void OnInitializingInternal(IViewModel viewModel);
//  	
//  	partial void OnInitializedInternal(IViewModel viewModel);
//      
//  	public void Deinitialize()
//  	{
//  	    if (ViewModel is null) return;
//  	
//  	    DeinitializeInternal();
//  	    ViewModel = null;
//  	}
//  	
//  	protected virtual void DeinitializeInternal()
//      {
//          OnDeinitializingInternal();
//              
//          __imageCachedBinder.UnbindSafely();
//          __imagesCachedBinder.UnbindSafely();
//          __propertyImageCachedPropertyBinder.UnbindSafely();
//          __propertyImagesCachedPropertyBinder.UnbindSafely();
//              
//          OnDeinitializedInternal();
//      }
//  	
//  	partial void OnDeinitializingInternal();
//  	
//  	partial void OnDeinitializedInternal();
//      
//  	private void InstantiateBinders()
//      {
//          OnInstantiatingBinders();

//          if (_image) __imageCachedBinder ??= new StarterKit.Unity.ImageSpriteBinder(_image);
//          
//          if (__imagesCachedBinder == null)
//          {
//              var __local_images = _images;
//              __imagesCachedBinder = new StarterKit.Unity.ImageSpriteBinder[__local_images.Length];
//  		    
//              for (var i = 0; i < __local_images.Length; i++)
//                  __imagesCachedBinder[i] = new StarterKit.Unity.ImageSpriteBinder(__local_images[i]);
//          }
//          
//          if (PropertyImage) __propertyImageCachedPropertyBinder ??= new ImageSpriteBinder(PropertyImage);
//          if (PropertyImages) __propertyImagesCachedPropertyBinder ??= new ImageSpriteBinder(PropertyImages);
//          
//          OnInstantiatedBinders();
//      }
//  	
//  	partial void OnInstantiatingBinders();
//  	
//  	partial void OnInstantiatedBinders();
//  }