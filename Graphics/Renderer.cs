using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;
using Tao.Platform.Windows;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        
        uint SunBufferID;//sun
        uint vertexBufferID; //background 

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;
        
        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 0.5f;
        float rotationAngle = 10;

        public float translationX=0, 
                     translationY=0, 
                     translationZ=0;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 CenterOfDrawing;

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");

            Gl.glClearColor(0.76f, 0.94f, 0.96f, 1);

            float[] AllVertices= {
                //sun
                10,10,0,//center
	1.0f,0.89f,0.01f,
16.9939f,10.2931f,0.0f,
1.0f,0.89f,0.01f,
16.9755f,10.5857f,0.0f,
1.0f,0.89f,0.01f,
16.9448f,10.8773f,0.0f,
1.0f,0.89f,0.01f,
16.902f,11.1674f,0.0f,
1.0f,0.89f,0.01f,
16.847f,11.4554f,0.0f,
1.0f,0.89f,0.01f,
16.7801f,11.7408f,0.0f,
1.0f,0.89f,0.01f,
16.7012f,12.0232f,0.0f,
1.0f,0.89f,0.01f,
16.6106f,12.3021f,0.0f,
1.0f,0.89f,0.01f,
16.5084f,12.5769f,0.0f,
1.0f,0.89f,0.01f,
16.3948f,12.8472f,0.0f,
1.0f,0.89f,0.01f,
16.27f,13.1124f,0.0f,
1.0f,0.89f,0.01f,
16.1341f,13.3723f,0.0f,
1.0f,0.89f,0.01f,
15.9875f,13.6262f,0.0f,
1.0f,0.89f,0.01f,
15.8304f,13.8737f,0.0f,
1.0f,0.89f,0.01f,
15.6631f,14.1145f,0.0f,
1.0f,0.89f,0.01f,
15.4859f,14.348f,0.0f,
1.0f,0.89f,0.01f,
15.299f,14.5739f,0.0f,
1.0f,0.89f,0.01f,
15.1028f,14.7918f,0.0f,
1.0f,0.89f,0.01f,
14.8976f,15.0013f,0.0f,
1.0f,0.89f,0.01f,
14.6839f,15.202f,0.0f,
1.0f,0.89f,0.01f,
14.462f,15.3936f,0.0f,
1.0f,0.89f,0.01f,
14.2322f,15.5757f,0.0f,
1.0f,0.89f,0.01f,
13.995f,15.748f,0.0f,
1.0f,0.89f,0.01f,
13.7508f,15.9103f,0.0f,
1.0f,0.89f,0.01f,
13.5f,16.0622f,0.0f,
1.0f,0.89f,0.01f,
13.2431f,16.2034f,0.0f,
1.0f,0.89f,0.01f,
12.9805f,16.3338f,0.0f,
1.0f,0.89f,0.01f,
12.7126f,16.453f,0.0f,
1.0f,0.89f,0.01f,
12.44f,16.561f,0.0f,
1.0f,0.89f,0.01f,
12.1631f,16.6574f,0.0f,
1.0f,0.89f,0.01f,
11.8824f,16.7421f,0.0f,
1.0f,0.89f,0.01f,
11.5985f,16.8151f,0.0f,
1.0f,0.89f,0.01f,
11.3117f,16.876f,0.0f,
1.0f,0.89f,0.01f,
11.0226f,16.9249f,0.0f,
1.0f,0.89f,0.01f,
10.7317f,16.9617f,0.0f,
1.0f,0.89f,0.01f,
10.4395f,16.9862f,0.0f,
1.0f,0.89f,0.01f,
10.1466f,16.9985f,0.0f,
1.0f,0.89f,0.01f,
9.8534f,16.9985f,0.0f,
1.0f,0.89f,0.01f,
9.56047f,16.9862f,0.0f,
1.0f,0.89f,0.01f,
9.2683f,16.9617f,0.0f,
1.0f,0.89f,0.01f,
8.97742f,16.9249f,0.0f,
1.0f,0.89f,0.01f,
8.68833f,16.876f,0.0f,
1.0f,0.89f,0.01f,
8.40154f,16.8151f,0.0f,
1.0f,0.89f,0.01f,
8.11756f,16.7421f,0.0f,
1.0f,0.89f,0.01f,
7.83688f,16.6574f,0.0f,
1.0f,0.89f,0.01f,
7.56f,16.561f,0.0f,
1.0f,0.89f,0.01f,
7.28739f,16.453f,0.0f,
1.0f,0.89f,0.01f,
7.01954f,16.3338f,0.0f,
1.0f,0.89f,0.01f,
6.75693f,16.2034f,0.0f,
1.0f,0.89f,0.01f,
6.5f,16.0622f,0.0f,
1.0f,0.89f,0.01f,
6.24921f,15.9103f,0.0f,
1.0f,0.89f,0.01f,
6.005f,15.748f,0.0f,
1.0f,0.89f,0.01f,
5.76781f,15.5757f,0.0f,
1.0f,0.89f,0.01f,
5.53803f,15.3936f,0.0f,
1.0f,0.89f,0.01f,
5.31609f,15.202f,0.0f,
1.0f,0.89f,0.01f,
5.10236f,15.0013f,0.0f,
1.0f,0.89f,0.01f,
4.89722f,14.7918f,0.0f,
1.0f,0.89f,0.01f,
4.70104f,14.5739f,0.0f,
1.0f,0.89f,0.01f,
4.51415f,14.348f,0.0f,
1.0f,0.89f,0.01f,
4.33688f,14.1145f,0.0f,
1.0f,0.89f,0.01f,
4.16955f,13.8737f,0.0f,
1.0f,0.89f,0.01f,
4.01245f,13.6262f,0.0f,
1.0f,0.89f,0.01f,
3.86585f,13.3723f,0.0f,
1.0f,0.89f,0.01f,
3.73002f,13.1124f,0.0f,
1.0f,0.89f,0.01f,
3.60518f,12.8472f,0.0f,
1.0f,0.89f,0.01f,
3.49156f,12.5769f,0.0f,
1.0f,0.89f,0.01f,
3.38937f,12.3021f,0.0f,
1.0f,0.89f,0.01f,
3.29876f,12.0232f,0.0f,
1.0f,0.89f,0.01f,
3.21992f,11.7408f,0.0f,
1.0f,0.89f,0.01f,
3.15297f,11.4554f,0.0f,
1.0f,0.89f,0.01f,
3.09803f,11.1674f,0.0f,
1.0f,0.89f,0.01f,
3.0552f,10.8773f,0.0f,
1.0f,0.89f,0.01f,
3.02455f,10.5857f,0.0f,
1.0f,0.89f,0.01f,
3.00614f,10.2931f,0.0f,
1.0f,0.89f,0.01f,
3f,10f,0.0f,
1.0f,0.89f,0.01f,
3.00614f,9.70687f,0.0f,
1.0f,0.89f,0.01f,
3.02455f,9.41426f,0.0f,
1.0f,0.89f,0.01f,
3.0552f,9.12267f,0.0f,
1.0f,0.89f,0.01f,
3.09803f,8.83262f,0.0f,
1.0f,0.89f,0.01f,
3.15297f,8.54462f,0.0f,
1.0f,0.89f,0.01f,
3.21992f,8.25917f,0.0f,
1.0f,0.89f,0.01f,
3.29876f,7.97678f,0.0f,
1.0f,0.89f,0.01f,
3.38937f,7.69793f,0.0f,
1.0f,0.89f,0.01f,
3.49157f,7.42313f,0.0f,
1.0f,0.89f,0.01f,
3.60518f,7.15284f,0.0f,
1.0f,0.89f,0.01f,
3.73002f,6.88755f,0.0f,
1.0f,0.89f,0.01f,
3.86585f,6.62772f,0.0f,
1.0f,0.89f,0.01f,
4.01245f,6.37381f,0.0f,
1.0f,0.89f,0.01f,
4.16955f,6.12626f,0.0f,
1.0f,0.89f,0.01f,
4.33688f,5.8855f,0.0f,
1.0f,0.89f,0.01f,
4.51415f,5.65196f,0.0f,
1.0f,0.89f,0.01f,
4.70104f,5.42606f,0.0f,
1.0f,0.89f,0.01f,
4.89722f,5.20817f,0.0f,
1.0f,0.89f,0.01f,
5.10236f,4.99869f,0.0f,
1.0f,0.89f,0.01f,
5.31609f,4.79798f,0.0f,
1.0f,0.89f,0.01f,
5.53803f,4.60641f,0.0f,
1.0f,0.89f,0.01f,
5.76781f,4.42429f,0.0f,
1.0f,0.89f,0.01f,
6.00501f,4.25195f,0.0f,
1.0f,0.89f,0.01f,
6.24921f,4.08971f,0.0f,
1.0f,0.89f,0.01f,
6.5f,3.93782f,0.0f,
1.0f,0.89f,0.01f,
6.75693f,3.79658f,0.0f,
1.0f,0.89f,0.01f,
7.01955f,3.66621f,0.0f,
1.0f,0.89f,0.01f,
7.28739f,3.54696f,0.0f,
1.0f,0.89f,0.01f,
7.56f,3.43903f,0.0f,
1.0f,0.89f,0.01f,
7.83688f,3.3426f,0.0f,
1.0f,0.89f,0.01f,
8.11756f,3.25786f,0.0f,
1.0f,0.89f,0.01f,
8.40154f,3.18495f,0.0f,
1.0f,0.89f,0.01f,
8.68833f,3.12399f,0.0f,
1.0f,0.89f,0.01f,
8.97742f,3.07509f,0.0f,
1.0f,0.89f,0.01f,
9.2683f,3.03835f,0.0f,
1.0f,0.89f,0.01f,
9.56047f,3.01381f,0.0f,
1.0f,0.89f,0.01f,
9.8534f,3.00153f,0.0f,
1.0f,0.89f,0.01f,
10.1466f,3.00154f,0.0f,
1.0f,0.89f,0.01f,
10.4395f,3.01381f,0.0f,
1.0f,0.89f,0.01f,
10.7317f,3.03835f,0.0f,
1.0f,0.89f,0.01f,
11.0226f,3.07509f,0.0f,
1.0f,0.89f,0.01f,
11.3117f,3.12399f,0.0f,
1.0f,0.89f,0.01f,
11.5985f,3.18495f,0.0f,
1.0f,0.89f,0.01f,
11.8824f,3.25786f,0.0f,
1.0f,0.89f,0.01f,
12.1631f,3.3426f,0.0f,
1.0f,0.89f,0.01f,
12.44f,3.43903f,0.0f,
1.0f,0.89f,0.01f,
12.7126f,3.54696f,0.0f,
1.0f,0.89f,0.01f,
12.9805f,3.66621f,0.0f,
1.0f,0.89f,0.01f,
13.2431f,3.79658f,0.0f,
1.0f,0.89f,0.01f,
13.5f,3.93782f,0.0f,
1.0f,0.89f,0.01f,
13.7508f,4.08971f,0.0f,
1.0f,0.89f,0.01f,
13.995f,4.25196f,0.0f,
1.0f,0.89f,0.01f,
14.2322f,4.42429f,0.0f,
1.0f,0.89f,0.01f,
14.462f,4.60641f,0.0f,
1.0f,0.89f,0.01f,
14.6839f,4.79799f,0.0f,
1.0f,0.89f,0.01f,
14.8976f,4.99869f,0.0f,
1.0f,0.89f,0.01f,
15.1028f,5.20817f,0.0f,
1.0f,0.89f,0.01f,
15.299f,5.42606f,0.0f,
1.0f,0.89f,0.01f,
15.4859f,5.65197f,0.0f,
1.0f,0.89f,0.01f,
15.6631f,5.88551f,0.0f,
1.0f,0.89f,0.01f,
15.8305f,6.12626f,0.0f,
1.0f,0.89f,0.01f,
15.9876f,6.37381f,0.0f,
1.0f,0.89f,0.01f,
16.1341f,6.62772f,0.0f,
1.0f,0.89f,0.01f,
16.27f,6.88756f,0.0f,
1.0f,0.89f,0.01f,
16.3948f,7.15284f,0.0f,
1.0f,0.89f,0.01f,
16.5084f,7.42313f,0.0f,
1.0f,0.89f,0.01f,
16.6106f,7.69794f,0.0f,
1.0f,0.89f,0.01f,
16.7012f,7.97678f,0.0f,
1.0f,0.89f,0.01f,
16.7801f,8.25917f,0.0f,
1.0f,0.89f,0.01f,
16.847f,8.54462f,0.0f,
1.0f,0.89f,0.01f,
16.902f,8.83262f,0.0f,
1.0f,0.89f,0.01f,
16.9448f,9.12267f,0.0f,
1.0f,0.89f,0.01f,
16.9755f,9.41426f,0.0f,
1.0f,0.89f,0.01f,
16.9939f,9.70687f,0.0f,
1.0f,0.89f,0.01f,
17f,10f,0.0f,
1.0f,0.89f,0.01f,
16.9939f,10.2931f,0.0f,
1.0f,0.89f,0.01f,



				//ground
				-1.0f * 200,-0.26f*100,-20f,
                0.88f,0.79f,0.34f,
                1.0f*100,-0.26f * 100,-20f,
                0.88f,0.79f,0.34f,
                1.0f*200,-1.0f *200,-20f,
                0.88f,0.79f,0.34f,
                -1.0f*200,-1.0f*200,-20f,
                0.88f,0.79f,0.34f,
				
			   //tree leg
					  -1.0f*100,-0.1f*100,-20f,
                       0.1f,0.1f,0.02f,
                       -0.8f * 70,-0.1f * 70,-20f,
                       0.1f,0.1f,0.02f,
                        -0.8f * 100,-0.8f * 100,-20f,
                       0.78f,0.43f,0.02f,
                       -1.0f * 100,-0.8f*100,-20f,
                       0.78f,0.43f,0.02f,

              //tree body
                       -1.0f * 100,0.6f*100,-20f,
                       0.13f,0.37f,0.13f,
                       -0.7f * 70,0.2f*60,-20f,
                       0.13f,0.27f,0.13f,
                       -0.7f * 70,-0.3f*85,-20f,
                       0.13f,0.27f,0.13f,
                       -1.0f * 100,-0.3f * 85,-20f,
                       0.13f,0.27f,0.13f,
	

				//tree traingls 
				-0.6f*70,0.04f*70,-20f,
                0.13f,0.27f,0.13f,
                -0.7f*70,0.2f*60,-20f,
                0.13f,0.27f,0.13f,
                -0.7f*70,0.0f*70,-20f,
                0.13f,0.27f,0.13f,

                -0.6f * 70,-0.15f*70,-20f,
                 0.13f,0.27f,0.13f,
                 -0.7f*70,0.05f*70,-20f,
                  0.13f,0.27f,0.13f,
                 -0.7f*70,-0.2f*70,-20f,
                  0.13f,0.27f,0.13f,

                   -0.7f * 70,-0.15f*70,-20f,
                   0.13f,0.27f,0.13f,
                   -0.6f * 75,-0.3f*85,-20f,
                   0.13f,0.27f,0.13f,
                   -0.7f * 70,-0.3f*85,-20f,
                   0.13f,0.27f,0.13f,

				  
			   //small pyramid 
				  0.3f*100,-0.6f*100,-20f,
                  0.8f,0.25f,0.1f,
                  0.25f*100,-0.2f*100,-20f,
                   0.8f,0.25f,0.1f,
                  0.4f*100,0.06f*100,-20f,
                   0.8f,0.25f,0.1f,

                   0.4f*100,0.06f*100,-20f,
                   0.87f,0.85f,0.10f,
                   0.9f*100,-0.55f*100,-20f,
                   0.87f,0.85f,0.10f,
                   0.3f*100,-0.6f*100,-20f,
                   0.65f,0.55f,0.00f,
				
				//big pyramid 
				 0.15f*100,0.2f*100,-20f,
                 0.85f,0.85f,0.10f,
                 -0.2f*100,-0.75f*100,-20f,
                  0.85f,0.85f,0.10f,
                  0.5f*100,-0.7f*100,-20f,
                  0.85f,0.85f,0.10f,

                   0.15f*100,0.2f*100,-20f,
                   0.8f,0.4f,0.0f,
                  -0.2f*100,-0.75f*100,-20f,
                  0.8f,0.4f,0.0f,
                  -0.4f*100,-0.6f*100,-20f,
                  0.8f,0.4f,0.0f,

            }; 

            vertexBufferID = GPU.GenerateBuffer(AllVertices);
            CenterOfDrawing = new vec3(20, -20, 10);


            SunBufferID = GPU.GenerateBuffer(AllVertices);
           

            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(0, 40, 60), // Camera is at (0,4,6), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 1, 1)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(44.999f, 4f / 3f, 0.5f, 100.0f);
            
            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

            timer.Start();
        }

        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

           //sun
            #region Animated SUN
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, SunBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);//vertex
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));//colors

            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, 152);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion
           
            //background
            #region drawing 

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, vertexBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_QUADS, 152, 12);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 164, 36);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion


        }


        public void Update()
        {

            timer.Stop();
            var deltaTime = timer.ElapsedMilliseconds/1000.0f;

            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
            transformations.Add(glm.translate(new mat4(1), -1.7f * CenterOfDrawing));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 1, 1)));
            transformations.Add(glm.translate(new mat4(1), 1.1f* CenterOfDrawing));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            ModelMatrix =  MathHelper.MultiplyMatrices(transformations);
            
            timer.Reset();
            timer.Start();
        }
        
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
