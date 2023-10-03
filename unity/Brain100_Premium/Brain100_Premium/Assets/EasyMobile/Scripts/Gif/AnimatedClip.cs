using UnityEngine;
using System.Collections;
using System;

namespace EasyMobile
{
    public class AnimatedClip : IDisposable
    {
        /// <summary>
        /// The actual width of this clip.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; private set; }

        /// <summary>
        /// The actual height of this clip.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; private set; }

        /// <summary>
        /// The actual FPS of this clip.
        /// </summary>
        /// <value>The frame per second.</value>
        public int FramePerSecond { get; private set; }

        /// <summary>
        /// The actual length of this clip (in seconds).
        /// </summary>
        /// <value>The length.</value>
        public float Length { get; private set; }

        /// <summary>
        /// The recorded frames.
        /// </summary>
        /// <value>The frames.</value>
        public RenderTexture[] Frames { get; private set; }

        // Mark whether this object is disposed.
        private bool isDisposed = false;

        public AnimatedClip(int width, int height, int fps, RenderTexture[] frames)
        {
            this.Width = width;
            this.Height = height;
            this.FramePerSecond = fps;
            this.Frames = frames;
            this.Length = (float)frames.Length / fps;
        }

        ~AnimatedClip()
        {
            // Would love to call the Dispose here, but unfortunately we can't
            // access the Release method of RenderTexture from the background
            // thread of the GC. Better warn the user to do the Dispose explicitly.
            // Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged)
        {
            if (!isDisposed)
            {
                if (disposeManaged)
                {
                    // Dispose managed resources. Not much to do in this case.
                }

                // Dispose unmanaged resources.
                if (this.Frames != null)
                {
                    foreach (var rt in Frames)
                    {
                        rt.Release();
                        UnityEngine.Object.Destroy(rt);
                    }

                    this.Frames = null;
                }

                isDisposed = true;
            }
        }
    }
}
