//
//  EMGifLib.h
//  EasyMobile
//
//  Copyright © 2017 SgLib Games. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "gif_export.h"

@interface EMGifLib : NSObject

#pragma mark - ObjC API

+ (bool) exportGif: (int) taskId
      withFilePath: (const NSString *) filepath
         withWidth: (int) width
        withHeight: (int) height
          withLoop: (int) loop
           withFps: (int) fps
  withSampleFactor: (int) sampleFac
    withFrameCount: (int) frameCount
     withImageData: (Color32 **) imageData
 exportingCallback: (GifExportingCallback) exportingCallback
  completeCallback: (GifExportCompletedCallback) completeCallback
         errorCode: (int *) error;

#pragma mark - C API
void _ExportGif(int taskId,
                const char *filepath,
                int width,
                int height,
                int loop,
                int fps,
                int sampleFac,
                int frameCount,
                Color32 **imageData,
                int queuePriority,
                GifExportingCallback exportingCallback,
                GifExportCompletedCallback completeCallback);

@end
