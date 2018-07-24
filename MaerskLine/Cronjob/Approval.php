<?php
	try
	{
		$connection = new PDO("sqlsrv:Server=maerskline2018ddacdbserver.database.windows.net;Database=MaerskLine2018DDAC_db", "jordansoo96", "Ravemaster96");
		$connection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
		$connection->setAttribute(PDO::SQLSRV_ATTR_ENCODING, PDO::SQLSRV_ENCODING_SYSTEM);
	}catch (Exception $e)
	{
		echo $e->getMessage();
		die('Connection Failed! Please try again.<br />');
	}
	try
	{
		$sql = "UPDATE Shippings SET status = 'Delivering' WHERE status = 'Approved';";
		$query = $connection->prepare($sql);
		$query->execute();
		$result = $query->fetchAll(PDO::FETCH_ASSOC);
	}catch (Exception $e)
	{
		die('Internal errors! Please try again.');
	}
?>