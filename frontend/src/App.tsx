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
import { AdminSmartDevice } from "./pages/AdminPages/AdminSmartDevice";
import { AdminLogisticCompanies } from "./pages/AdminPages/AdminLogisticCompanies";
import { PrivateCompanyProfile } from "./pages/PrivateCompanyPages/PrivateCompanyProfile";
import { PrivateCompanyStatistics } from "./pages/PrivateCompanyPages/PrivateCompanyStatistics";
import { PrivateCompanyCancelledOrders } from "./pages/PrivateCompanyPages/PrivateCompanyCancelledOrders";
import { PrivateCompanyDeliveredOrders } from "./pages/PrivateCompanyPages/PrivateCompanyDeliveredOrders";
import { PrivateCompanyActiveOrders } from "./pages/PrivateCompanyPages/PrivateCompanyActiveOrders";
import { PrivateCompanyShowOrderInfo } from "./pages/PrivateCompanyPages/PrivateCompanyShowOrderInfo";
import { PrivateCompanyCreateOrder } from "./pages/PrivateCompanyPages/PrivateCompanyCreateOrder";
import { LogisticCompanyProfile } from "./pages/LogisticCompanyPages/LogisticCompanyProfile";
import { LogisticCompanyStatistics } from "./pages/LogisticCompanyPages/LogisticCompanyStatistics";
import { LogisticCompanySmartDevices } from "./pages/LogisticCompanyPages/LogisticCompanySmartDevices";
import { LogisticCompanyChangeAdministrators } from "./pages/LogisticCompanyPages/LogisticCompanyChangeAdministrators";
import { LogisticCompaniesAdministratorProfile } from "./pages/LogisticCompanyAdministratorPages/LogisticCompaniesAdministratorProfile";

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
					{/* Admins Pages */}
					<Route path="/SystemAdminProfile" element={<SystemAdminProfile />} />
					<Route path="/AdminSubscriptionTypes" element={<AdminSubscriptionTypes />} />
					<Route path="/AdminSensors" element={<AdminSensor />} />
					<Route path="/AdminSmartDevices" element={<AdminSmartDevice />} />
					<Route path="/AdminLogisticCompanies" element={<AdminLogisticCompanies />} />
					{/* Private Company Pages */}
					<Route path="/PrivateCompanyProfile" element={<PrivateCompanyProfile />} />
					<Route path="/PrivateCompanyStatistics" element={<PrivateCompanyStatistics />} />
					<Route path="/PrivateCompanyCancelledOrders" element={<PrivateCompanyCancelledOrders />} />
					<Route path="/PrivateCompanyDeliveredOrders" element={<PrivateCompanyDeliveredOrders />} />
					<Route path="/PrivateCompanyActiveOrders" element={<PrivateCompanyActiveOrders />} />
					<Route path="/PrivateCompanyShowOrderInfo/:id" element={<PrivateCompanyShowOrderInfo />} />
					<Route path="/PrivateCompanyCreateOrder" element={<PrivateCompanyCreateOrder />} />
					{/* Logistic Company Pages */}
					<Route path="/LogisticCompanyProfile" element={<LogisticCompanyProfile />} />
					<Route path="/LogisticCompanyStatistics" element={<LogisticCompanyStatistics />} />
					<Route path="/LogisticCompanySmartDevices" element={<LogisticCompanySmartDevices />} />
					<Route path="/LogisticCompanyChangeAdministrators" element={<LogisticCompanyChangeAdministrators />} />
					{/* Logistic Company Administrators Pages */}
					<Route
						path="/LogisticCompaniesAdministratorProfile"
						element={<LogisticCompaniesAdministratorProfile />}
					/>
					{/* Default Router */}
					<Route path="/" element={<Navigate to="/Home" />} />
				</Routes>
			</BrowserRouter>
		</div>
	);
};

