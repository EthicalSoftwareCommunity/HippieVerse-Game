extends Spatial

export(float) var radius
onready var center = $Center
var spawnPointX:float
var spawnPointZ:float
func _ready():
	spawn_platform(false)
	var timer = Timer.new()
	add_child(timer)
	timer.wait_time = 2
	timer.connect("timeout", self, "spawn_platform", [true])
	timer.start()
	
func spawn_platform(offset:bool):
	
	var random = RandomNumberGenerator.new()
	random.randomize()
	if offset == false:
		spawnPointX = random.randf_range(-radius, radius)
	else:
		spawnPointX = radius
		while abs(spawnPointX) >= radius:
			spawnPointX = random.randf_range(-radius, radius)
			spawnPointX = clamp(spawnPointX, -1*radius, radius)	
	var verticalPositionCoefficient = random.randi_range(-1,1)
	if verticalPositionCoefficient == 0:
		verticalPositionCoefficient = 1
	spawnPointZ = sqrt(radius*radius - spawnPointX*spawnPointX) * verticalPositionCoefficient
	
	translation = Vector3(spawnPointX, 0 ,spawnPointZ)
	
	look_at(Vector3(0,0,0), Vector3.UP)
	rotate_y(deg2rad(-90))

	#spawn_platform(true)
