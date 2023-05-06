import { useState, useEffect, FC } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { LogisticCompanyRates, RatesService } from "../../api/services/RatesService";
import { RatesList } from "../../components/RatesList";
import { GoogleMapChoosePoint } from "../../components/GoogleMapChoosePoint";
import { Cargo, CargoDto, CargosService } from "../../api/services/CargosService";
import { Address, AddressesService } from "../../api/services/AddressesService";
import { OrderDto, OrdersService } from "../../api/services/OrdersService";
import { DataTimeService } from "../../helpers/DataTimeService";

export const PrivateCompanyCreateOrder: FC = () => {
	const [logisticCompanyRates, setLogisticCompanyRates] = useState<LogisticCompanyRates[] | null>(null);
	const [selectedLogisticCompanyId, setSelectedLogisticCompanyId] = useState<number>(0);

	const [selectedEstimatedDate, setSelectedEstimatedDate] = useState<Date>(new Date());
	const [name, setName] = useState<string>("");
	const [weight, setWeight] = useState<number>(0);
	const [length, setLength] = useState<number>(0);
	const [width, setWidth] = useState<number>(0);
	const [height, setHeight] = useState<number>(0);
	const [description, setDescription] = useState<string>("");

	const [isMetric, setIsMetric] = useState(true);

	const [latStartPoint, setLatStartPoint] = useState<number>(0);
	const [lngStartPoint, setLngStartPoint] = useState<number>(0);

	const [latEndPoint, setLatEndPoint] = useState<number>(0);
	const [lngEndPoint, setLngEndPoint] = useState<number>(0);

	const handleCreateItem = async (): Promise<void> => {
		if (!verifyOrder()) {
			// TODO Language
			alert("Order data is not correct!");
			return;
		}
		const cargoId: number | null = await createCargo();
		if (cargoId === null) {
			return;
		}
		const startAddress: Address | null = await createAddress(latStartPoint, lngStartPoint);
		if (startAddress === null) {
			return;
		}
		const endAddress: Address | null = await createAddress(latEndPoint, lngEndPoint);
		if (endAddress === null) {
			return;
		}

		let orderDto: OrderDto = {
			privateCompanyId: parseInt(localStorage["userId"]),
			logisticCompanyId: selectedLogisticCompanyId,
			cargoId: cargoId,
			startDeliveryAddressId: startAddress.addressId === undefined ? 0 : startAddress.addressId,
			endDeliveryAddressId: endAddress.addressId === undefined ? 0 : endAddress.addressId,
			estimatedDeliveryDateTime: DataTimeService.prototype.getUTCDataByLocalData(selectedEstimatedDate.toISOString())
		};

		if (orderDto.startDeliveryAddressId === 0 || orderDto.endDeliveryAddressId === 0) {
			return;
		}

		await new Promise(resolve => setTimeout(resolve, 1000));

		try {
			const response: any = await OrdersService.prototype.create(orderDto);
			console.log(response);
		} catch (err: any) {
			// TODO Language
			alert("Order can't be created");
			return;
		}
		window.location.href = "/PrivateCompanyActiveOrders";
	};

	const createCargo = async (): Promise<number | null> => {
		try {
			let cargoDto: CargoDto = {
				name: name,
				weight: weight,
				length: length,
				width: width,
				height: height,
				description: description,
				weightMeasureUnit: isMetric ? "kg" : "lb",
				sizeMeasureUnit: isMetric ? "cm" : "inch"
			};

			const response: number | null = await CargosService.prototype.create(cargoDto);
			if (response === null) return null;
			return response;
		} catch (error) {
			alert("Can't create Cargo");
		}
		return null;
	};

	const createAddress = async (lat: number, lng: number): Promise<Address | null> => {
		try {
			let address: Address = {
				addressName: "",
				latitude: lat,
				longitute: lng
			};

			const response: Address | null = await AddressesService.prototype.create(address);
			if (response === null) return null;
			return response;
		} catch (error) {
			alert("Can't create Address");
		}
		return null;
	};

	const getLogisticCompanyRates = async (): Promise<void> => {
		try {
			const response: LogisticCompanyRates[] | null = await RatesService.prototype.getRates();
			if (response === null) return;
			setLogisticCompanyRates(response);
		} catch (err) {}
	};

	function setStartPoint(lat: number, lng: number): void {
		setLatStartPoint(lat);
		setLngStartPoint(lng);
	}

	function setEndPoint(lat: number, lng: number): void {
		setLatEndPoint(lat);
		setLngEndPoint(lng);
	}

	const verifyOrder = (): boolean => {
		return (
			isSelectedLogisticCompanyIdCorrect() && isCargoCorrect() && isStartAddressCorrect() && isEndAddressCorrect()
		);
	};

	const isSelectedLogisticCompanyIdCorrect = (): boolean => {
		return selectedLogisticCompanyId !== 0;
	};

	const isCargoCorrect = (): boolean => {
		return weight > 0 && length > 0 && width > 0 && height > 0;
	};

	const isStartAddressCorrect = (): boolean => {
		return latStartPoint !== 0 && lngStartPoint !== 0;
	};

	const isEndAddressCorrect = (): boolean => {
		return latEndPoint !== 0 && lngEndPoint !== 0;
	};

	const getWeightMeasureUnit = (): string => {
		return isMetric ? "kg" : "lb";
	};

	const getSizeMeasureUnit = (): string => {
		return isMetric ? "cm" : "inch";
	};

	const timeZoneOffset = new Date().getTimezoneOffset() * 60000;
	const minDate = new Date(Date.now() - timeZoneOffset).toISOString().slice(0, -8);

	useEffect(() => {
		getLogisticCompanyRates();
	}, []);
	return (
		<div className="PrivateCompanyCreateOrder container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Create order</h1>
					</div>
				</header>
				<div>
					<div>
						<h3>Select logistic company</h3>
					</div>
					<div>
						{logisticCompanyRates !== null ? (
							<RatesList
								logisticCompanyRates={logisticCompanyRates === null ? [] : logisticCompanyRates}
								selectedItemId={selectedLogisticCompanyId}
								onSelectItem={setSelectedLogisticCompanyId}
							/>
						) : (
							<p>No data</p>
						)}
					</div>
				</div>
				<div className="container">
					<div className="mt-5 mb-5">
						<Form>
							<Form.Group>
								<Form.Label>Choose estimated delivery date and time for this order</Form.Label>
								<Form.Control
									type="datetime-local"
									value={selectedEstimatedDate.toISOString().slice(0, -8)}
									min={minDate}
									onChange={e => setSelectedEstimatedDate(new Date(e.target.value))}
									onKeyDown={e => e.preventDefault()}
								/>
							</Form.Group>
						</Form>
					</div>
					<div className="mt-5 mb-5">
						<h3>Fill in cargo information</h3>
						<Form>
							<Form.Group>
								<Form.Label>Cargo name</Form.Label>
								<Form.Control type="text" value={name} onChange={e => setName(e.target.value)} />
							</Form.Group>
							<Form.Group>
								<Form.Label>Description</Form.Label>
								<Form.Control type="text" value={description} onChange={e => setDescription(e.target.value)} />
							</Form.Group>
							<Form.Group>
								<Form.Label>Choose type of metric</Form.Label>
								<div className="mb-3 mt-3">
									<select
										value={isMetric ? "metric" : "imperial"}
										onChange={e => setIsMetric(e.target.value === "metric")}>
										<option value="metric">Metric</option>
										<option value="imperial">Imperial</option>
									</select>
								</div>
							</Form.Group>
							<Form.Group>
								<Form.Label>Weight, {`${getWeightMeasureUnit()}`}</Form.Label>
								<Form.Control
									type="number"
									value={weight}
									min={0}
									onChange={e => setWeight(parseInt(e.target.value))}
								/>
							</Form.Group>
							<Form.Group>
								<Form.Label>Length, {`${getSizeMeasureUnit()}`}</Form.Label>
								<Form.Control
									type="number"
									value={length}
									min={0}
									onChange={e => setLength(parseInt(e.target.value))}
								/>
							</Form.Group>
							<Form.Group>
								<Form.Label>Width, {`${getSizeMeasureUnit()}`}</Form.Label>
								<Form.Control type="number" value={width} onChange={e => setWidth(parseInt(e.target.value))} />
							</Form.Group>
							<Form.Group>
								<Form.Label>Height, {`${getSizeMeasureUnit()}`}</Form.Label>
								<Form.Control
									type="number"
									value={height}
									min={0}
									onChange={e => setHeight(parseInt(e.target.value))}
								/>
							</Form.Group>
						</Form>
						<div className="mb-5 mt-5">
							<div>
								<h3>Choose start delivery address</h3>
							</div>
							<div>
								<GoogleMapChoosePoint onTypeItem={setStartPoint} />
							</div>
						</div>
						<div className="mb-5 mt-5">
							<div>
								<h3>Choose end delivery address</h3>
							</div>
							<div>
								<GoogleMapChoosePoint onTypeItem={setEndPoint} />
							</div>
						</div>
					</div>
					<Button onClick={() => handleCreateItem()} variant="outline-primary">
						Create
					</Button>
				</div>
			</div>
		</div>
	);
};
