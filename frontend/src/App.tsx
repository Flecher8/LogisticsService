import { FC } from "react";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import { Menu } from "./components/Menu";
import { Home } from "./pages/Home";
import { Contacts } from "./pages/Contacts";
import { Login } from "./pages/Login";
import { Registration } from "./pages/Registration";
import { SystemAdminProfile } from "./pages/AdminPages/SystemAdminProfile";
import { AdminSubscriptionTypes } from "./pages/AdminPages/AdminSubscriptionTypes";
import { AdminSensor } from "./pages/AdminPages/AdminSensor";

export const App: FC = () => {
	return (
		<div className="App">
			<BrowserRouter>
				<Menu />
				<Routes>
					<Route path="/Home" element={<Home />} />
					<Route path="/Contacts" element={<Contacts />} />
					<Route path="/Login" element={<Login />} />
					<Route path="/Registration" element={<Registration />} />
					<Route path="/SystemAdminProfile" element={<SystemAdminProfile />} />
					<Route path="/AdminSubscriptionTypes" element={<AdminSubscriptionTypes />} />
					<Route path="/AdminSensors" element={<AdminSensor />} />
					{/* Default Router */}
					<Route path="/" element={<Navigate to="/Home" />} />
				</Routes>
			</BrowserRouter>
		</div>
	);
};

