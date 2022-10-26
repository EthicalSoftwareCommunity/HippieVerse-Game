extends Obstacle

class_name CircleObtacle
export(float) var speed = 3.0
export(float) var radius = 2.5
var x:float = 0
var z:float = 0
var d:float = 0
var direction = 1
func _ready():
	randomize()
	direction = rand_range(-1,1)
	d = rand_range(-180,180)
	pass # Replace with function body.

func _physics_process(delta):
	circle_moving(delta)
	
func circle_moving(delta):
	#x^2 + z^2 = 2.8^2
	d+=delta * sign(direction)
	x = sin(d*speed) * radius
	z = cos(d*speed) * radius
	
	translation = Vector3(x,0,z)

