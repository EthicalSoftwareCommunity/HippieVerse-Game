extends Obstacle

class_name PinPongObtacle

export(float) var speed = 3.0
export(float) var radius = 2.5
export(float) var directionOffset = 2.0

var nextX:float
var nextZ:float
var direction:Vector3
var targetPoint:Vector3
var xCoef:int = 1
var zCoef:int = 1
var circleNumber:float = 0

var x:float = sin(circleNumber*speed) * radius
var z:float = cos(circleNumber*speed) * radius
func _ready():
	translation = Vector3(x,0,z)
	change_direction()
	pass # Replace with function body.

func _physics_process(delta):
	pinpong_moving(delta)
	
func pinpong_moving(delta):
	x += direction.x * delta * speed
	z += direction.z * delta * speed
	if ((abs(x) >= abs(targetPoint.x)) and (sign(x) == sign(direction.x))) or	((abs(z) >= abs(targetPoint.z)) and (sign(z) == sign(direction.z))): 
		change_direction()
	#if x == -radius or x == radius or z == 
	translation = Vector3(x,0,z)

func change_direction():
	randomize()
	circleNumber = rand_range(0, 180)
	targetPoint = Vector3(sin(circleNumber*speed) * radius, 0, cos(circleNumber*speed) * radius)
	direction = Vector3(x,0,z).direction_to(targetPoint).normalized()
	
