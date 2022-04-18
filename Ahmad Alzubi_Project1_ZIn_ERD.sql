CREATE TABLE Configurations(
    config_Lock CHAR(1) DEFAULT 'X' NOT NULL,
    email VARCHAR2(50),
    phone VARCHAR2(50),
    locationUrl VARCHAR2(200),
    locationEmbed VARCHAR2(300),
    facebookUrl VARCHAR2(100),
    instagramUrl VARCHAR2(100),
    twitterUrl VARCHAR2(100),
    linkedinUrl VARCHAR2(100),
    pinterestUrl VARCHAR2(100),
    websiteUrl VARCHAR2(100),
    address_main VARCHAR2(150),
    address_sec VARCHAR2(150),
    
    CONSTRAINT Config_PK PRIMARY KEY (config_Lock),
    CONSTRAINT CK_Configurations_Locked CHECK (config_Lock='X')
)

CREATE TABLE Balance(
    balance_Lock CHAR(1) DEFAULT 'X' NOT NULL,
    profitRate FLOAT(126) DEFAULT 0.1,
    balanceValue FLOAT(126) DEFAULT 0.0,
    
    CONSTRAINT Balance_PK PRIMARY KEY (balance_Lock),
    CONSTRAINT CK_Balance_Locked CHECK (balance_Lock='X')
)

CREATE TABLE CreditCard(
    card_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    cardNumber NUMBER NOT NULL UNIQUE,
    cardKey NUMBER NOT NULL,
    expiryDate DATE NOT NULL,
    balance FLOAT(126),
    
    CONSTRAINT CK_PK PRIMARY KEY (card_ID)
)

CREATE TABLE UserRole(
    role_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    roleIndex Number,
    roleName VARCHAR2(50),
    roleDescription VARCHAR2(250),
    
    CONSTRAINT UserRole_PK PRIMARY KEY (role_ID)
)

CREATE TABLE EndUser(
    user_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    fName VARCHAR2(50),
    lName VARCHAR2(50),
    country VARCHAR2(50),
    city VARCHAR2(50),
    gender CHAR(1),
    birthday DATE,
    registerDate DATE,
    user_username VARCHAR2(50) NOT NULL UNIQUE, --UNIQUE
    user_password VARCHAR2(50) NOT NULL,
    user_email VARCHAR2(50) NOT NULL UNIQUE,  --UNIQUE
    USERIMAGE BLOB,
    imageExtension VARCHAR2(20),
    
    role_ID Number(*,0),
    creditC_ID Number(*,0),
    
    CONSTRAINT EndUser_PK PRIMARY KEY (user_ID),
    CONSTRAINT EndUser_FK1 FOREIGN KEY(creditC_ID) REFERENCES CreditCard(card_ID) ON DELETE SET NULL,
    CONSTRAINT chk_gender CHECK (gender IN ('M', 'F'))
)

CREATE TABLE ShopReview(
    review_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    rate Number(*,0),
    reviewComment VARCHAR2(200),
    reviewDate DATE,
    
    reviewer_ID Number(*,0) UNIQUE,
    
    CONSTRAINT ShopReview_PK PRIMARY KEY (review_ID),
    CONSTRAINT ShopReview_FK1 FOREIGN KEY(reviewer_ID) REFERENCES EndUser(user_ID) ON DELETE CASCADE,
    CONSTRAINT chk_rate CHECK (rate IN (1, 2, 3, 4, 5))
)

CREATE TABLE ItemType(
    type_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    typeName VARCHAR2(50) UNIQUE,
    typeDescription VARCHAR2(250),
    createDate DATE NOT NULL,
    
    CONSTRAINT ItemType_PK PRIMARY KEY (type_ID),
    CONSTRAINT chk_typeName CHECK (typeName IN ('Logo', 'Photo'))
)

CREATE TABLE ItemCategory(
    cate_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    cateName VARCHAR2(50),
    createDate DATE NOT NULL,
    
    CONSTRAINT ItemCategory_PK PRIMARY KEY (cate_ID)
)

CREATE TABLE Item(
    item_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    itemFile BLOB NOT NULL,
    itemExtension VARCHAR2(20) NOT NULL,
    itemName VARCHAR2(50),
    uploadDate DATE,
    createDate DATE,
    width Number,
    height Number,
    price FLOAT(126),
    itemDescription VARCHAR2(200),
    takenLocation VARCHAR2(50),
    noViews NUMBER DEFAULT 0 NOT NULL,
    
    noLikes Numbe DEFAULT 0 NOT NULL,
    noPurchases Number DEFAULT 0 NOT NULL,
    popularity Number DEFAULT 0 NOT NULL,
    
    type_ID Number(*,0),
    category_ID Number(*,0),
    owner_ID Number(*,0) NOT NULL,
    
    CONSTRAINT Item_PK PRIMARY KEY (item_ID),
    CONSTRAINT Item_FK1 FOREIGN KEY(type_ID) REFERENCES ItemType(type_ID) ON DELETE CASCADE,
    CONSTRAINT Item_FK2 FOREIGN KEY(category_ID) REFERENCES ItemCategory(cate_ID) ON DELETE SET NULL,
    CONSTRAINT Item_FK3 FOREIGN KEY(owner_ID) REFERENCES EndUser(user_ID) ON DELETE CASCADE
)

CREATE TABLE UserLikeItem(
    like_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    likeDate DATE,
    
    user_ID Number(*,0),
    item_ID Number(*,0),
    
    CONSTRAINT Like_PK PRIMARY KEY (like_ID),
    CONSTRAINT Like_FK1 FOREIGN KEY(user_ID) REFERENCES EndUser(user_ID) ON DELETE CASCADE,
    CONSTRAINT Like_FK2 FOREIGN KEY(item_ID) REFERENCES Item(item_ID) ON DELETE CASCADE
)

CREATE TABLE UserPurchaseItem(
    purchase_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    purchaseDate DATE,
    paymentTotal FLOAT(126),
    relatedSiteRate FLOAT(126),
    
    buyer_ID Number(*,0),
    item_ID Number(*,0),
    
    CONSTRAINT Purchase_PK PRIMARY KEY (purchase_ID),
    CONSTRAINT Purchase_FK1 FOREIGN KEY(buyer_ID) REFERENCES EndUser(user_ID) ON DELETE SET NULL,
    CONSTRAINT Purchase_FK2 FOREIGN KEY(item_ID) REFERENCES Item(item_ID) ON DELETE SET NULL
)

CREATE TABLE UserTransaction(
    uTransaction_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    uTransactionDate DATE,
    paymentTotal FLOAT(126),
    relatedSiteRate FLOAT(126),
    uTransactionValue FLOAT(126),
    UTdescription VARCHAR2(200),
    
    purchase_ID Number(*,0) UNIQUE,
    from_ID Number(*,0),
    to_ID Number(*,0),
    
    CONSTRAINT uTransaction_PK PRIMARY KEY (uTransaction_ID),
    CONSTRAINT uTransaction_FK1 FOREIGN KEY(purchase_ID) REFERENCES UserPurchaseItem(purchase_ID) ON DELETE SET NULL,
    CONSTRAINT uTransaction_FK2 FOREIGN KEY(from_ID) REFERENCES EndUser(user_ID) ON DELETE SET NULL,
    CONSTRAINT uTransaction_FK3 FOREIGN KEY(to_ID) REFERENCES EndUser(user_ID) ON DELETE SET NULL
)

CREATE TABLE BalanceTransaction(
    bTransaction_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    bTransactionDate DATE,
    paymentTotal FLOAT(126),
    relatedSiteRate FLOAT(126),
    bTransactionValue FLOAT(126),
    BTdescription VARCHAR2(200),
    
    purchase_ID Number(*,0) UNIQUE,
    from_ID Number(*,0),
    balanceLock CHAR(1) DEFAULT 'X',
    
    CONSTRAINT bTransaction_PK PRIMARY KEY (bTransaction_ID),
    CONSTRAINT bTransaction_FK1 FOREIGN KEY(purchase_ID) REFERENCES UserPurchaseItem(purchase_ID) ON DELETE SET NULL,
    CONSTRAINT bTransaction_FK2 FOREIGN KEY(from_ID) REFERENCES EndUser(user_ID) ON DELETE SET NULL,
    CONSTRAINT bTransaction_FK3 FOREIGN KEY(balanceLock) REFERENCES Balance(balance_Lock) ON DELETE SET NULL
)

CREATE TABLE AccessLoger(
    access_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    access_Page VARCHAR2(50),
    access_counter Number(*,0) DEFAULT 0,
    access_date DATE,
    access_visitor VARCHAR2(50),
    
    CONSTRAINT Access_PK PRIMARY KEY (access_ID)
)

CREATE TABLE LoginLoger(
    login_ID Number(*,0) GENERATED ALWAYS AS IDENTITY(START with 1 INCREMENT by 1),
    login_counter Number(*,0) DEFAULT 0,
    login_date DATE,
    
    login_user_ID Number(*,0),
    
    CONSTRAINT LoginLoger_PK PRIMARY KEY (login_ID),
    CONSTRAINT LoginLoger_FK1 FOREIGN KEY(login_user_ID) REFERENCES EndUser(user_ID) ON DELETE CASCADE
)



