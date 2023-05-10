import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { Order, OrdersService } from "../../api/services/OrdersService";
import { OrderCard } from "../../components/OrderCard";
import { LogisticCompaniesAdministratorsService } from "../../api/services/LogisticCompaniesAdministratorsService";
import { LogisticCompaniesAdministratorPanel } from "../../components/LogisticCompaniesAdministratorPanel";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompanyAdministratorCancelledOrders: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [orders, setOrders] = useState<Order[] | null>();

	const getOrders = async (): Promise<void> => {
		try {
			const logisticCompanyId: number =
				await LogisticCompaniesAdministratorsService.prototype.getLogisticCompanyId();
			const response: Order[] | null = await OrdersService.prototype.getCancelledOrdersByLogisticCompany(
				logisticCompanyId
			);
			setOrders(response);
		} catch (err) {}
	};

	useEffect(() => {
		getOrders();
	}, []);
	return (
		<div className="LogisticCompanyAdministratorCancelledOrders container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompaniesAdministratorPanel />
			</div>
			<div className="d-flex flex-column">
				<header>
					<div className="text-center mt-5">
						<h1>{t("Cancelled orders")}</h1>
					</div>
				</header>
				<div className="d-flex flex-column align-items-start flex-fill flex-wrap">
					{orders ? (
						orders.map(order => (
							<div key={order.orderId}>
								<Link
									key={order.orderId}
									to={`/LogisticCompanyAdministratorShowOrderInfo/${order.orderId}`}
									style={{ textDecoration: "none", color: "black" }}>
									<OrderCard order={order} />
								</Link>
							</div>
						))
					) : (
						<p>{t("No data")}</p>
					)}
				</div>
			</div>
		</div>
	);
};
