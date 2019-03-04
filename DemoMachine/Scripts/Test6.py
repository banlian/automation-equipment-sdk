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
		
		pass
		
	elif state == RunningState.Running:
		while True:
			t.Log(t.Name)
			t.Log("Run Script Running", LogLevel.Debug)

		pass
	else:
		print('state not equal to any')
		
except Exception as e:
	t.Log(str(e), LogLevel.Error)
	print(e)
