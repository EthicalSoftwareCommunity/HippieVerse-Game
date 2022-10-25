extends Spatial

export(float) var radius
export(float) var rotateAngle = 90.0
export(float) var rotateSpeed = 300

onready var center = $"../Center"
var halfRotateAngle:float = rotateAngle/2
var leftBorderAngle:float
var rightBorderAngle:float
var currentRotateAngle:float = 0
var leftRightCoef:int = 1

func _ready():

	spawn_platform()
	
func spawn_platform():
	
	var random = RandomNumberGenerator.new()
	random.randomize()
	var spawnPointX:float
	var spawnPointZ:float
	
	spawnPointX = random.randf_range(-1*radius, radius)
	var verticalPositionCoefficient = random.randi_range(-1,1)
	if verticalPositionCoefficient == 0:
		verticalPositionCoefficient = 1
	spawnPointZ = sqrt(radius*radius - spawnPointX*spawnPointX) * verticalPositionCoefficient
	
	translation = Vector3(spawnPointX, 0 ,spawnPointZ)
	
	var direction = translation.direction_to(center.translation)
	look_at(Vector3(0,0,0), Vector3.UP)
	rotate_y(deg2rad(-90))
	rightBorderAngle =rad2deg(global_rotation.y) + halfRotateAngle
	leftBorderAngle =rad2deg(global_rotation.y) - halfRotateAngle


func _physics_process(delta):
	rotate_platform(delta)
	pass

func rotate_platform(delta:float):
	currentRotateAngle = rad2deg(global_rotation.y)
	currentRotateAngle =  clamp(currentRotateAngle+rotateSpeed*leftRightCoef*delta, leftBorderAngle, rightBorderAngle)
	if currentRotateAngle == rightBorderAngle or currentRotateAngle == leftBorderAngle :
		leftRightCoef*=-1;
		
	rotate_y(deg2rad(rotateSpeed*leftRightCoef*delta))
	pass
