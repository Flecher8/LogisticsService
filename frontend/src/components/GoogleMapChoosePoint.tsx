import { useState, useEffect, useRef, useMemo, FC } from "react";
import { GoogleMap, LoadScript, Marker, useLoadScript, MarkerF } from "@react-google-maps/api";

interface GoogleMapsProps {
	onTypeItem: (lat: number, lng: number) => void;
}

export const GoogleMapChoosePoint: FC<GoogleMapsProps> = ({ onTypeItem }) => {
	const { isLoaded } = useLoadScript({
		googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY || ""
	});

	const [markerPosition, setMarkerPosition] = useState({ lat: 0, lng: 0 });

	const containerStyle = {
		height: "300px"
	};

	const handleClick = (event: any) => {
		const lt: number = event.latLng.lat();
		const lg: number = event.latLng.lng();

		setMarkerPosition({ lat: lt, lng: lg });

		onTypeItem(lt, lg);
	};

	const center = useMemo(() => ({ lat: 0, lng: 0 }), []);

	useEffect(() => {
		setMarkerPosition({ lat: 0, lng: 0 });
	}, []);

	return (
		<div className="GoogleMaps">
			{isLoaded && (
				<GoogleMap mapContainerStyle={containerStyle} center={center} zoom={2} onClick={handleClick}>
					{markerPosition.lat === 0 && markerPosition.lng === 0 ? null : <MarkerF position={markerPosition} />}
				</GoogleMap>
			)}
		</div>
	);
};
