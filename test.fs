/*
{
  "DESCRIPTION" : "ShapeMaker",
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "time_in",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0,
      "MIN" : 0
    },

    {
      "VALUES" : [
        0,
        1,
        2,
        3,
        4,
        5
      ],
      "NAME" : "fill_method",
      "TYPE" : "long",
      "DEFAULT" : 4,
      "LABELS" : [
        "straight",
        "iterate",
        "random",
        "distribute",
        "blend",
        "none"
      ]
    },
    {
      "NAME" : "fill_modifier",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "MIN" : 0
    },
    {
      "VALUES" : [
        0,
        1,
        2
      ],
      "NAME" : "blend_mode",
      "TYPE" : "long",
      "DEFAULT" : 1,
      "LABELS" : [
        "add",
        "over",
        "multiply"
      ]
    },
    {
      "NAME" : "fill0",
      "TYPE" : "color",
      "DEFAULT" : [
        0.97657740116119385,
        0.67969787120819092,
        0.18750286102294922,
        1
      ],
      "LABEL" : "Fill 1"
    },
    {
      "NAME" : "fill1",
      "TYPE" : "color",
      "DEFAULT" : [
        1,
        0.42745098471641541,
        0,
        1
      ],
      "LABEL" : "Fill 2"
    },
    {
      "NAME" : "fill2",
      "TYPE" : "color",
      "DEFAULT" : [
        0,
        0.54117649793624878,
        0.49803921580314636,
        1
      ],
      "LABEL" : "Fill 3"
    },
    {
      "NAME" : "fill3",
      "TYPE" : "color",
      "DEFAULT" : [
        0.9686274528503418,
        0.93333333730697632,
        0.87450981140136719,
        1
      ],
      "LABEL" : "Fill 4"
    },
    {
      "NAME" : "fill4",
      "TYPE" : "color",
      "DEFAULT" : [
        0.47450980544090271,
        0.47450980544090271,
        0.47450980544090271,
        1
      ],
      "LABEL" : "Fill 5"
    },
    {
      "NAME" : "stroke_color",
      "TYPE" : "color",
      "DEFAULT" : [
        1,
        1,
        1,
        1
      ],
      "LABEL" : "Stroke Color"
    },
    {
      "NAME" : "bg_color",
      "TYPE" : "color",
      "DEFAULT" : [
        1,
        0.42745098471641541,
        0,
        1
      ],
      "LABEL" : "Background Color"
    },
    {
      "NAME" : "alpha",
      "TYPE" : "bool",
      "DEFAULT" : false,
      "LABEL" : "Alpha?"
    },
    {
      "NAME" : "bg_col",
      "TYPE" : "bool",
      "DEFAULT" : true,
      "LABEL" : "Use Fill Color?"
    },
    {
      "NAME" : "bgColor_modifier",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.65763306617736816,
      "MIN" : 0
    },
    {
      "VALUES" : [
        0,
        1,
        2
      ],
      "NAME" : "shape_type",
      "TYPE" : "long",
      "DEFAULT" : 0,
      "LABELS" : [
        "circle",
        "rectangle",
        "triangle"
      ]
    },
    {
      "VALUES" : [
        0,
        1,
        2
      ],
      "NAME" : "shape_method",
      "TYPE" : "long",
      "DEFAULT" : 0,
      "LABELS" : [
        "straight",
        "iterate",
        "random"
      ]
    },
    {
      "NAME" : "shape_modifier",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0,
      "MIN" : 0
    },
    {
      "NAME" : "size_x",
      "TYPE" : "float",
      "MAX" : 3,
      "DEFAULT" : 0.066499710083007812,
      "MIN" : 0
    },
    {
      "NAME" : "size_y",
      "TYPE" : "float",
      "MAX" : 3,
      "DEFAULT" : 0.35066536068916321,
      "MIN" : 0
    },
    {
      "NAME" : "offset",
      "TYPE" : "float",
      "MAX" : 5,
      "DEFAULT" : 1.6535211801528931,
      "MIN" : 0
    },
    {
      "NAME" : "frequency",
      "TYPE" : "float",
      "MAX" : 5,
      "DEFAULT" : 1.3366290330886841,
      "MIN" : 0
    },
    {
      "NAME" : "amplitude",
      "TYPE" : "float",
      "MAX" : 5,
      "DEFAULT" : 0.47407671809196472,
      "MIN" : 0
    },
    {
      "NAME" : "rounding_amount",
      "TYPE" : "float",
      "MAX" : 0.5,
      "DEFAULT" : 0,
      "MIN" : 0
    },
    {
      "NAME" : "stroke_size",
      "TYPE" : "float",
      "MAX" : 0.5,
      "DEFAULT" : 0,
      "MIN" : 0
    },
    {
      "NAME" : "count",
      "TYPE" : "float",
      "MAX" : 500,
      "DEFAULT" : 41.819301605224609,
      "MIN" : 0
    }
  ],
  "VSN" : "2.0",
  "CREDIT" : "@colin_movecraft"
}
*/

#define PI 3.14159265358979
#define TAU PI * 2.0

///UTILS////

//1D random generator
float rand(float x){
	return fract(sin(x)*100000.0);
}

//remapping

float map(float number, float inputMin , float inputMax ){
	float differenceInputs = inputMax - inputMin;
	float differenceNum = number- inputMin;
	return differenceNum/differenceInputs; 
}

float remap(float number, float inputMin,float inputMax, float outputMin, float outputMax){
	return outputMin+(outputMax-outputMin)*(number-inputMin)/(inputMax-inputMin);
}


vec3 colorMix(float colorCount, float index,vec4 colors[5]){
		float stepVal = floor(min(max(index * colorCount, 0.0 ) , colorCount ));
		float interval = 1.0/colorCount ;
		float begin = stepVal * interval;
		float end = (stepVal+1.0) * interval;
			
		return mix(
				colors[int(stepVal)].rgb,
				colors[int(stepVal)+1].rgb,
				map(index,begin,end)
			);}
				
		
///Shape Functions///

vec4 drawShape(int shapeType, vec2 screenSpace, vec2 pos, vec2 size, float rounding, bool fillState, vec3 fillColor, vec3 strokeColor, float strokeSize){
	
	vec4 color = vec4(0.0);
	float floatSize = ((size.x+size.y)/2.0)*.2;
	vec2 dist = vec2(0.0);
	
	
	if (shapeType ==0){
	//circle distance function
	float circle = length(screenSpace-pos);
	
 	//fill assignment
	float ellipseFillStep = step(circle+strokeSize*.5,floatSize*3.0);
	color.rgb += ellipseFillStep * fillColor.rgb;
	
	//stroke assignment
	float ellipseStrokeStep = step(floatSize*3.0, circle+strokeSize * 0.5) - step(floatSize*3.0, circle - strokeSize * 0.5);
	color.rgb += ellipseStrokeStep * strokeColor.rgb;
	
	if (fillState == false){
		color.a = ellipseStrokeStep;
	}else{	
		color.a = ellipseFillStep + ellipseStrokeStep;
	}
	
	} else if (shapeType ==1){

	//rectangle distance function
	vec2 dist = abs(screenSpace-pos)-(size-vec2(rounding));
	float rect = min(max(dist.x, dist.y),0.0) + length(max(dist,0.0))-rounding;
	
	 //step
	 float rectFillStep = 1.0 - float(step(0.0,rect+strokeSize *.5));
	 
	 //fill assignment
	color.rgb += rectFillStep * fillColor.rgb;
	
	//stroke assignment
	float rectStrokeStep = step(1.0 - float(step(0.0,rect+strokeSize *.5)), 0.0 ) -  step(1.0 - float(step(0.0,rect-strokeSize *.5)), 0.0 );
	
	color.rgb += rectStrokeStep * strokeColor.rgb; 
	
	if (fillState == false){
		color.a = rectStrokeStep;
	}else{	
		color.a = rectFillStep + rectStrokeStep;
	}
	
	} else if (shapeType ==2){
	
	//triangle distance function
    vec2 dist2 = abs(screenSpace-pos);
    vec2 screenSpaceOffset = screenSpace-pos;
    float tri = max(dist2.x * 0.866025 + screenSpaceOffset.y* 0.5, -screenSpaceOffset.y * 0.5) - floatSize * 0.5;

    //fill assignment
	float triangleFillStep = step(tri+strokeSize*.5,floatSize);
	color.rgb += triangleFillStep * fillColor.rgb;
	
	//stroke assignment
	float triangleStrokeStep = step(floatSize, tri+strokeSize * 0.5) - step(floatSize, tri - strokeSize * 0.5);
	color.rgb += triangleStrokeStep * strokeColor.rgb;
	
	if (fillState == false){
		color.a = triangleStrokeStep;
	}else{	
		color.a = triangleFillStep + triangleStrokeStep;
	}
	
	}
	return color;
}


///Global Containers///

//an array for the colors
vec4 colors[5];

void main(){
	
//fill the array
colors[0] = fill0;
colors[1] = fill1;
colors[2] = fill2;
colors[3] = fill3;
colors[4] = fill4;

//get the screen space
vec2 screenSpace = isf_FragNormCoord;

//correct the aspect ratio
screenSpace -= 0.5; 
screenSpace.x *= RENDERSIZE.x/RENDERSIZE.y;

//create a color vector and alpha to accumulate to
vec4 color = vec4(0.0,0.0,0.0,0.0);

//needs to be white if in multiply mode
if (blend_mode == 2){
color.rgb = vec3(1.0,1.0,1.0);
}


//main loop

for (int i = 0; i < 500; ++i){
	
	if (i >= int(count)){
		break;
	}else{
		
	
	/*some presets
	float xPos = sin( ((time_in*TAU)*frequency) + (iteration*offset) )*amplitude * sin(TIME + iteration*10.0);
	float yPos = cos( ((time_in*TAU)*frequency) + (iteration* offset) )*amplitude;
	
	//radius and size
	float sizeX =  .5 - index + (sin(iteration)*.25 ) * rect_width;
	float sizeY = (sin(time_in*TAU+iteration))*index * .25 * rect_height;
	*/
	
	//iterator variables
	float iteration = float(i);
	float index = iteration/count;
	
	//number of shapes and colors
	float colorNum = 5.0;
	float shapeNum = 3.0;
	
	//Defaults & Placeholders
	vec3 fillColor = vec3(0.0); //empty fill of black
	vec3 strokeColor = stroke_color.rgb;
	bool fillState = true; //default fill state of true
	vec4 shape = vec4(0.0); //empty shape vector
	
	//fft
	//vec4 fft = IMG_NORM_PIXEL(fftImage, vec2(index*.5,0.0));
	//float fftVal = (fft.r + fft.g + fft.b) / 3.0;
	
	//oscillators
	float osc = sin(time_in*TAU+iteration)*index * .25;
	float osc2 = cos( ((time_in*TAU)*frequency) + (iteration* offset) )*amplitude;
	
	//positions
	float xPos = index-.5 ;
	float yPos =  0.0;
	vec2 position = vec2(xPos,yPos);
	
	//Size and shape
	float rounding = rounding_amount;
	float strokeSize = stroke_size;
	float sizeX =  size_x;
	float sizeY =  size_y ;
	vec2 size = vec2(sizeX,sizeY);
	

	//Fill Assignment
	
	if (fill_method == 0){
		
		//straight fill from fill Slider
		int colorPick = int(floor(remap(fill_modifier,0.0,1.0,0.0,colorNum-1.0)));
		fillColor = colors[colorPick].rgb;
		
	}else if (fill_method == 1){
		
		//modulo to assign in order
		int colorIndex = int(mod(float(i)+remap(fill_modifier,0.0,1.0,0.0,colorNum-1.0),float(colorNum)));
		fillColor = colors[colorIndex].rgb;
		
	} else if (fill_method == 2){
		
		//pick a random number
		fillColor = colors[int(rand(iteration+fill_modifier)*colorNum)].rgb;
		
	} else if (fill_method ==3){
		
		//distribute
		int colorPick = int(floor(index*(remap(fill_modifier,0.0,1.0,1.0,colorNum))));
		fillColor = colors[colorPick].rgb;
		
	}else if (fill_method ==4){
		
		//mix
		fillColor = colorMix(remap(fill_modifier,0.0,1.0,1.0,colorNum-1.0),index,colors);
		
	}else if (fill_method ==5){
		
		//no fill
		fillState = false;
		
	}

	//Shape Assignments
	if (shape_method == 0){
		
		//straight shape with offset
		int shapeType = int(mod(( float(shape_type) + remap(shape_modifier,0.0,1.0,0.0,shapeNum-1.0) ),shapeNum) );
		shape = drawShape(shapeType,screenSpace,position, size, rounding, fillState, fillColor,strokeColor,strokeSize);
		
		} else if (shape_method == 1){
				
		//iterate through shapes with offset
		 int typeIndex = int(mod(float(i)+remap(shape_modifier,0.0,1.0,0.0,shapeNum-1.0),float(shapeNum)));
		 shape = drawShape(typeIndex,screenSpace,position, size, rounding, fillState, fillColor,strokeColor,strokeSize);
		 
	} else if (shape_method ==2){
		
		//random shapes with seed
		int typeRandom = int(rand(iteration+shape_modifier)*shapeNum);
		shape = drawShape(typeRandom,screenSpace,position, size, rounding, fillState, fillColor,strokeColor,strokeSize);	
		
	}
	
	
	//Rendering
	
	//Add blend mode
	if (blend_mode==0){
	color += shape;
	}else if (blend_mode == 1){
		
	//over blend mode
	//get the alpha
	float mask = color.a += shape.a;
	
	//if there is an intersection, flip it back with a triangle function
	mask = max(1.0-abs(1.0-mask),0.0);
		
	//mulitply by the mask
	shape.rgb *= clamp(mask,0.0,1.0);
	
	//accumulate with color
	color.rgb += shape.rgb;
	
	}else if (blend_mode ==2){
	
	//mulitply blend mode
	float matte = 1.0-shape.a; //flip the alpha to white
	shape.rgb +=matte; //add it to the RGB
	color.rgb *= shape.rgb; //multiply with accumulated color
	
	//build an alpha channel and accumulate
	float mask = shape.a;	
	color.a += mask;
	}
	}

}

//Build Background
vec3 bg = vec3(0.0);
bg_col ? bg = colors[int(remap(bgColor_modifier,0.0,1.0,0.0,4.0))].rgb : bg = bg_color.rgb;
vec3 bg2 = 1.0-vec3(clamp(color.a,0.0,1.0) ) ;
vec3 bg3 = bg * bg2;
vec4 colorbg = vec4(color.rgb += bg3,1.0);

//alpha condition
alpha ? color : color = colorbg;

//assign to Color
gl_FragColor = vec4(color);

}