// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <Commercially/Commercially.h>
#import <UIKit/UIKit.h>

#import "EmailField.h"
#import "RoundedUIButton.h"
#import "PasswordField.h"

@interface LoginController : UIViewController {
	EmailField *_EmailField;
	UIScrollView *_KeyboardScrollView;
	RoundedUIButton *_LoginButton;
	PasswordField *_PasswordField;
}

@property (nonatomic, retain) IBOutlet EmailField *EmailField;

@property (nonatomic, retain) IBOutlet UIScrollView *KeyboardScrollView;

@property (nonatomic, retain) IBOutlet RoundedUIButton *LoginButton;

@property (nonatomic, retain) IBOutlet PasswordField *PasswordField;

@end
