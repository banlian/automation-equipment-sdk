from Automation.FrameworkExtension.common import *
from Automation.FrameworkExtension.stateMachine import *
from Automation.FrameworkExtension import *
import clr
clr.ImportExtensions(motionDriver)

import sys
print(sys.path)
import time


try:
	print(state)

	if state == RunningState.Resetting:
		t.Log(t.Name)
		t.Log("Run Script Resetting", LogLevel.Debug)
		Cy1.SetDo(t, False)
		LightGreen.SetDo(False)
		
		t.Log("laser measure val:"+str(Laser1.Measure()[0]))
		
		t.Log("Run Script Resetting Finish", LogLevel.Debug)
		pass
		
	elif state == RunningState.Running:
	
		isset = 0
		while True:
	
			t.Log(t.Name)
			t.Log("Run Script Running", LogLevel.Debug)
			
			if isset == 0:
				isset = 1
				LightGreen.SetDo(True)
				Cy1.SetDo(t)
				print('set cy1')
			else:
				isset = 0
				Cy1.SetDo(t, False)
				LightGreen.SetDo(False)
				print('reset cy1')
			
			time.sleep(1)
			
		pass
	else:
		print('state not equal to any')
		
except Exception as e:
	t.Log(str(e), LogLevel.Error)
	print(e)